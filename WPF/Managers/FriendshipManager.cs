using WPF.Models;
using WPF.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace WPF.Managers;

public static class FriendshipManager
{
	public static async Task DeleteFriendship(User user1, User user2)
	{
		using var context = new AppDbContext();
		using var transaction = context.Database.BeginTransaction();
		try
		{
			context.Users.Attach(user1);
			context.Users.Attach(user2);

			Friendship? fs = await context.Friendships.FirstOrDefaultAsync(fs =>
				(fs.Friend1Id == user1.Id && fs.Friend2Id == user2.Id) ||
				(fs.Friend1Id == user2.Id && fs.Friend2Id == user1.Id)
			);

			if (fs == null)
			{
				MessageBox.Show("Friendship not found.");
				return;
			}

			context.Friendships.Remove(fs);

			user1.Friends.Remove(user2);
			user2.Friends.Remove(user1);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
		}
		catch
		{
			await transaction.RollbackAsync();
			MessageBox.Show("Something went wrong. Please, try again");
		}
	}
}
