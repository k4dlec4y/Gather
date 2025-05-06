using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class InviteManager
{
	public static async Task<string> SendInvite(User sender, User receiver, Event @event, string content)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			if (await context.Invites.AnyAsync(i =>
				i.FromId == sender.Id &&
				i.ToId == receiver.Id &&
				i.EventId == @event.Id))
				return "You have already sent an equivalent invite!";

			var dbSender = await context.Users.FirstOrDefaultAsync(u => u.Id == sender.Id);
			var dbReceiver = await context.Users
				.Include(u => u.Invites)
				.FirstOrDefaultAsync(u => u.Id == receiver.Id);
			var dbEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == @event.Id);

			if (dbSender == null || dbReceiver == null || dbEvent == null)
				return "One or more required entities could not be found.";

			if (dbEvent.Participants.Any(p => p.Id == dbReceiver.Id))
			{
				return $"{dbReceiver.Username} is already a participant!";
			}

			var invite = new Invite
			{
				FromId = dbSender.Id,
				ToId = dbReceiver.Id,
				EventId = dbEvent.Id,
				Content = content
			};

			await context.Invites.AddAsync(invite);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return "";
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			Debug.WriteLine($"{ex.Message}");
			Debug.WriteLine($"{ex.InnerException?.Message}");
			return ex.InnerException == null ? ex.Message : ex.InnerException.Message;
		}
	}

	public static async Task<string> AcceptInvite(Invite invite)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var dbInvite = await context.Invites
				.Include(i => i.To)
				.Include(i => i.Event)
				.ThenInclude(e => e.Participants)
				.AsNoTracking()
				.FirstOrDefaultAsync(i => i.Id == invite.Id);

			if (dbInvite == null)
				return "Could not find invite in the database";

			if (invite.Event.Participants.Any(p => p.Id == invite.To.Id))
			{
				return $"You are already a participant of this event!";
			}

			context.Invites.Attach(dbInvite);
			context.Users.Attach(dbInvite.From);
			context.Users.Attach(dbInvite.To);
			context.Events.Attach(dbInvite.Event);

			context.Invites.Remove(dbInvite);
			dbInvite.To.Invites.Remove(dbInvite);

			dbInvite.Event.Participants.Add(dbInvite.To);

			var message = new Message
			{
				FromId = dbInvite.To.Id,
				ToId = dbInvite.From.Id,
				Content = $"{dbInvite.To.Username} accepted your invite for {dbInvite.Event.Name} event!"
			};
			await context.Messages.AddAsync(message);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();

			

			return "";
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			Debug.WriteLine($"AcceptInvite - {ex.Message}");
			Debug.WriteLine($"AcceptInvite - {ex.InnerException?.Message}");
			return ex.InnerException == null ? ex.Message : ex.InnerException.Message;
		}
	}

	public static async Task<string> DeleteInvite(Invite invite)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var dbInvite = await context.Invites
				.Include(i => i.To)
				.AsNoTracking()
				.FirstOrDefaultAsync(i => i.Id == invite.Id);

			if (dbInvite == null)
				return "Could not find ivnite in the database";

			context.Invites.Attach(dbInvite);
			context.Users.Attach(dbInvite.To);
			context.Users.Attach(dbInvite.From);

			context.Invites.Remove(dbInvite);
			dbInvite.To.Invites.Remove(dbInvite);

			var message = new Message
			{
				FromId = dbInvite.To.Id,
				ToId = dbInvite.From.Id,
				Content = $"{dbInvite.To.Username} declined your invite for {dbInvite.Event.Name} event!"
			};
			await context.Messages.AddAsync(message);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return "";
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			Debug.WriteLine($"DeleteInvite - {ex.Message}");
			Debug.WriteLine($"DeleteInvite - {ex.InnerException?.Message}");
			return ex.InnerException == null ? ex.Message : ex.InnerException.Message;
		}
	}
}
