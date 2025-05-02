using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class MessageManager
{
	public static async Task<bool> SendMessage(User sender, User receiver, string content)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();

		try
		{
			var message = new Message
			{
				From = sender,
				FromId = sender.Id,
				To = receiver,
				ToId = receiver.Id,
				Content = content
			};

			context.Users.Attach(sender);
			context.Users.Attach(receiver);

			await context.Messages.AddAsync(message);

			receiver.Inbox.Add(message);

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

	public static async Task<bool> DeleteMessage(Message message)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();

		try
		{
			context.Messages.Attach(message);
			context.Users.Attach(message.To);

			context.Messages.Remove(message);

			message.To.Inbox.Remove(message);

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
