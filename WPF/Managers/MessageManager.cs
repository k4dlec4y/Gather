using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class MessageManager
{
	public static async Task<bool> SendMessage
	(
		User sender, User receiver, string content
	)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var dbSender = await context.Users
				.FirstOrDefaultAsync(u => u.Id == sender.Id);
			var dbReceiver = await context.Users
				.Include(u => u.Invites)
				.FirstOrDefaultAsync(u => u.Id == receiver.Id);

			if (dbSender == null || dbReceiver == null)
			{
				return false;
			}

				var message = new Message
			{
				FromId = dbSender.Id,
				ToId = dbReceiver.Id,
				Content = content
			};

			await context.Messages.AddAsync(message);

			receiver.Inbox.Add(message);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			Debug.WriteLine($"{ex.Message}");
			Debug.WriteLine($"{ex.InnerException?.Message}");
			return false;
		}
	}

	public static async Task<bool> DeleteMessage(Message message)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var dbMessage = await context.Messages
				.FirstOrDefaultAsync(m => m.Id == message.Id);

			if (dbMessage == null)
			{
				return false;
			}

			context.Messages.Remove(dbMessage);

			message.To.Inbox.Remove(message);

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
