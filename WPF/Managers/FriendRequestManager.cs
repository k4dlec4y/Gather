using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class FriendRequestManager
{
	public static async Task<bool> SendFriendRequest(
		User sender,
		User receiver,
		string content)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var friendRequest = new FriendRequest
			{
				FromId = sender.Id,
				ToId = receiver.Id,
				Content = content
			};

			context.TrackEntity(sender);
			context.TrackEntity(receiver);

			await context.FriendRequests.AddAsync(friendRequest);
			receiver.FriendRequests.Add(friendRequest);

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

	public static async Task<bool> ContainsFriendRequest(User sender, User receiver)
	{
		using var context = new AppDbContext();
		return await context.FriendRequests
			.AsNoTracking()
			.AnyAsync(f => f.FromId == sender.Id && f.ToId == receiver.Id);
	}

	public static async Task<bool> AcceptFriendRequest(FriendRequest friendRequest)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			context.Attach(friendRequest);
			context.Attach(friendRequest.To);

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
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			Debug.WriteLine(ex.InnerException?.Message);
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
			context.Attach(friendRequest);
			context.Attach(friendRequest.To);

			context.FriendRequests.Remove(friendRequest);
			friendRequest.To.FriendRequests.Remove(friendRequest);

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
