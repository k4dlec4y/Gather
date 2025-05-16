using WPF.Models;
using WPF.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WPF.Managers;

public static class FriendshipManager
{
	public static async Task<bool> DeleteFriendship(User user1, User user2)
	{
		using var context = new AppDbContext();
		using var transaction = context.Database.BeginTransaction();
		try
		{
			var dbuser1 = await context.Users
				.Include(u => u.Friends)
				.FirstOrDefaultAsync(u => u.Id == user1.Id);

			if (dbuser1 == null)
			{
				Debug.WriteLine("User 1 was not found.");
				return false;
			}

			var dbuser2 = await context.Users
				.Include(u => u.Friends)
				.FirstOrDefaultAsync(u => u.Id == user2.Id);

			if (dbuser2 == null)
			{
				Debug.WriteLine("User 1 was not found.");
				return false;
			}

			// they are bidirectional, so they are two
			var fs = context.Friendships.Where(fs =>
				(fs.Friend1Id == user1.Id && fs.Friend2Id == user2.Id) ||
				(fs.Friend1Id == user2.Id && fs.Friend2Id == user1.Id)
			);

			if (fs.Count() <= 0)
			{
				MessageBox.Show("Friendship not found.");
				return false;
			}

			context.Friendships.RemoveRange(fs);
			dbuser1.Friends.Remove(dbuser2);
			dbuser2.Friends.Remove(dbuser1);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			Debug.WriteLine(ex.InnerException?.Message);
			await transaction.RollbackAsync();
			return false;
		}
	}
}
