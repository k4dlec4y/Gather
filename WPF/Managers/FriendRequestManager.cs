using WPF.Data;
using WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace WPF.Managers;

public static class FriendRequestManager
{
	public static async Task<bool> SendFriendRequest(User sender, User receiver, string content)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();

		try
		{
			var friendRequest = new FriendRequest
			{
				From = sender,
				FromId = sender.Id,
				To = receiver,
				ToId = receiver.Id,
				Content = content
			};

			context.Users.Attach(sender);
			context.Users.Attach(receiver);

			await context.FriendRequests.AddAsync(friendRequest);

			receiver.FriendRequests.Add(friendRequest);

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

	public static async Task<bool> ContainsFriendRequest(User sender, User receiver)
	{
		using var context = new AppDbContext();

		var friendRequest = await context.FriendRequests
			.Include(f => f.From)
			.Include(f => f.To)
			.FirstOrDefaultAsync(f => f.FromId == sender.Id && f.ToId == receiver.Id);

		return friendRequest != null;
	}

	public static async Task<bool> AcceptFriendRequest(FriendRequest friendRequest)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();

		try
		{
			context.Users.Update(friendRequest.From);
			context.Users.Update(friendRequest.To);

			context.FriendRequests.Remove(friendRequest);

			context.Friendships.Add(new Friendship
			{
				Friend1 = friendRequest.From,
				Friend2 = friendRequest.To
			});

			friendRequest.From.Friends.Add(friendRequest.To);
			friendRequest.To.Friends.Add(friendRequest.From);
			friendRequest.To.FriendRequests.Remove(friendRequest);

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

	public static async Task<bool> DeleteFriendRequest(FriendRequest friendRequest)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();

		try
		{
			context.FriendRequests.Attach(friendRequest);
			context.Users.Attach(friendRequest.To);

			context.FriendRequests.Remove(friendRequest);

			friendRequest.To.FriendRequests.Remove(friendRequest);

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
