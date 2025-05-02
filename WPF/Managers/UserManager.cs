using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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

	public static async Task<(bool success, User? user)> GetUser(string username)
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

		return user != null ? (true, user) : (false, null);
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

	public static async Task RemoveUser(User user)
	{
		using var context = new AppDbContext();
		context.Users.Remove(user);
		await context.SaveChangesAsync();
	}

	public static async Task<bool> UpdateUser(User user)
	{
		using var context = new AppDbContext();
		try
		{
			context.Users.Update(user);
			await context.SaveChangesAsync();
			return true;
		}
		catch (DbUpdateException)
		{
			return false;
		}
	}
}
