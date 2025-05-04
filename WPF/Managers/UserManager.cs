using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class UserManager
{
	public static async Task<ObservableCollection<User>> GetUsers()
	{
		using var context = new AppDbContext();
		return new ObservableCollection<User>(await context.Users
			.Include(u => u.EventsToAttend)
			.Include(u => u.Friends)
			.Include(u => u.Inbox)
				.ThenInclude(m => m.From)
			.Include(u => u.Invites)
				.ThenInclude(i => i.From)
			.Include(u => u.Invites)
				.ThenInclude(i => i.Event)
			.Include(u => u.FriendRequests)
				.ThenInclude(f => f.From)
			.ToListAsync());
	}

	public static async Task<User?> GetUser(string username)
	{
		using var context = new AppDbContext();
		var user = await context.Users
			.Include(u => u.EventsToAttend)
			.Include(u => u.Friends)
			.Include(u => u.Inbox)
				.ThenInclude(m => m.From)
			.Include(u => u.Invites)
				.ThenInclude(i => i.From)
			.Include(u => u.Invites)
				.ThenInclude(i => i.Event)
			.Include(u => u.FriendRequests)
				.ThenInclude(f => f.From)
			.FirstOrDefaultAsync(u => u.Username == username);

		return user;
	}

	public static async Task<bool> ContainsUser(string username)
	{
		using var context = new AppDbContext();
		var user = await context.Users
			.FirstOrDefaultAsync(u => u.Username == username);

		return user != null;
	}

	public static async Task AddUser(User user)
	{
		using var context = new AppDbContext();
		await context.Users.AddAsync(user);
		await context.SaveChangesAsync();
	}

	public static async Task<bool> RemoveUser(User user)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			context.Users.Attach(user);

			// Remove friendships
			var friendships = context.Users
				.Where(u => u.Friends.Contains(user))
				.ToList();
			foreach (var friend in friendships)
			{
				friend.Friends.Remove(user);
			}

			Debug.WriteLine("1here");

			// Remove friend requests
			var friendRequests = context.FriendRequests
				.Where(fr => fr.FromId == user.Id || fr.ToId == user.Id);
			context.FriendRequests.RemoveRange(friendRequests);

			Debug.WriteLine("2here");

			// Remove invites
			var invites = context.Invites
				.Where(i => i.FromId == user.Id || i.ToId == user.Id);
			context.Invites.RemoveRange(invites);

			Debug.WriteLine("3here");

			// Remove messages
			var messages = context.Messages
				.Where(m => m.FromId == user.Id || m.ToId == user.Id);
			context.Messages.RemoveRange(messages);

			Debug.WriteLine("4here");

			// Remove participations in events
			var events = context.Events
				.Where(e => e.Participants.Contains(user))
				.ToList();
			foreach (var eventItem in events)
			{
				eventItem.Participants.Remove(user);
			}

			Debug.WriteLine("5here");

			// Remove him as organizer of events, if he is one
			var eventOrganizers = context.EventOrganizers
				.Where(e => e.Username == user.Username);
			if (!eventOrganizers.IsNullOrEmpty())
			{
				context.EventOrganizers.RemoveRange(eventOrganizers);
			}

			Debug.WriteLine("6here");

			// Finally, remove the user
			context.Users.Remove(user);
			Debug.WriteLine("7here");

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception e)
		{
			Debug.WriteLine(e.InnerException?.Message);
			await transaction.RollbackAsync();
			return false;
		}
	}

	public static async Task<bool> UpdateUser(User user)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			context.Users.Update(user);
			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch
		{
			await transaction.RollbackAsync();
			return false;
		}
	}
}
