using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class UserManager
{
	public static async Task<ObservableCollection<User>> GetUsers()
	{
		using var context = new AppDbContext();
		return new ObservableCollection<User>(await context.Users
			.AsNoTracking()
			.Include(u => u.EventsToAttend)
			.Include(u => u.Friends)
				.ThenInclude(f => f.EventsToAttend).AsNoTracking()
			.Include(u => u.Inbox)
				.ThenInclude(m => m.From)
			.Include(u => u.Invites)
				.ThenInclude(i => i.From)
			.Include(u => u.Invites)
				.ThenInclude(i => i.Event)
			.Include(u => u.FriendRequests)
				.ThenInclude(f => f.From)
			.AsNoTracking()
			.ToListAsync()
		);
	}

	public static async Task<User?> GetUser(string username)
	{
		using var context = new AppDbContext();
		return await context.Users
			.AsNoTracking()
			.Include(u => u.EventsToAttend)
			.Include(u => u.Friends)
				.ThenInclude(f => f.EventsToAttend).AsNoTracking()
			.Include(u => u.Inbox)
				.ThenInclude(m => m.From)
			.Include(u => u.Invites)
				.ThenInclude(i => i.From)
			.Include(u => u.Invites)
				.ThenInclude(i => i.Event)
			.Include(u => u.FriendRequests)
				.ThenInclude(f => f.From)
			.FirstOrDefaultAsync(u => u.Username == username);
	}

	public static async Task<bool> ContainsUser(string username)
	{
		using var context = new AppDbContext();
		return await context.Users
			.AsNoTracking()
			.AnyAsync(u => u.Username == username);
	}

	public static async Task AddUser(User user)
	{
		using var context = new AppDbContext();
		await context.Users.AddAsync(user);
		await context.SaveChangesAsync();
	}

	public static async Task<bool> RemoveUser(User user)
	{
		if (user.Username == "admin")
		{
			return false;
		}

		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			// remove friendships
			var friendships = context.Friendships
				.Where(fs =>
				fs.Friend1Id == user.Id || fs.Friend2Id == user.Id
			);
			context.Friendships.RemoveRange(friendships);

			// remove friend requests
			var friendRequests = context.FriendRequests
				.Where(fr => fr.FromId == user.Id || fr.ToId == user.Id);
			context.FriendRequests.RemoveRange(friendRequests);

			// remove invites
			var invites = context.Invites
				.Where(i => i.FromId == user.Id || i.ToId == user.Id);
			context.Invites.RemoveRange(invites);

			// remove messages
			var messages = context.Messages
				.Where(m => m.FromId == user.Id || m.ToId == user.Id);
			context.Messages.RemoveRange(messages);

			// remove participations in events
			var events = context.Events
				.Where(e => e.Participants.Contains(user));
			foreach (var eventItem in events)
			{
				eventItem.Participants.Remove(user);
			}

			// remove him as organizer of events, if he is one
			var eo = await context.EventOrganizers
				.FirstOrDefaultAsync(e => e.Username == user.Username);
			if (eo != null)
			{
				context.EventOrganizers.Remove(eo);
				context.Events.RemoveRange(eo.Events);
			}

			// remove the user
			context.Users.Remove(user);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			Debug.WriteLine(ex.Message);
			Debug.WriteLine(ex.InnerException?.Message);
			return false;
		}
	}

	public static async Task<bool> UpdateUser(User user)
	{
		if (user.Username == "admin")
		{
			return false;
		}

		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			context.Users.Attach(user);
			context.Users.Update(user);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			Debug.WriteLine(ex.Message);
			Debug.WriteLine(ex.InnerException?.Message);
			return false;
		}
	}
}
