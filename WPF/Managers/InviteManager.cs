using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class InviteManager
{
	public static async Task<bool> SendInvite(User sender, User receiver, Event @event, string content)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();

		try
		{
			var invite = new Invite
			{
				From = sender,
				FromId = sender.Id,
				To = receiver,
				ToId = receiver.Id,
				Event = @event,
				EventId = @event.Id,
				Content = content

			};

			context.Users.Attach(sender);
			context.Users.Attach(receiver);
			context.Events.Attach(@event);

			await context.Invites.AddAsync(invite);

			receiver.Invites.Add(invite);

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

	public static async Task<bool> DeleteInvite(Invite invite)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();

		try
		{
			context.Invites.Attach(invite);
			context.Users.Attach(invite.To);

			context.Invites.Remove(invite);

			invite.To.Invites.Remove(invite);

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
