using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class ParticipationManager
{
	public static async Task<bool> AddParticipation(Event @event, User user)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var dbEvent = await context.Events
				.Include(e => e.Participants)
				.FirstOrDefaultAsync(e => e.Id == @event.Id);

			var dbUser = await context.Users
				.FindAsync(user.Id);

			if (dbEvent == null || dbUser == null)
			{
				Debug.WriteLine("Event not found.");
				return false;
			}

			if (dbEvent.Participants.Contains(dbUser))
			{
				return false;
				
			}
			dbEvent.Participants.Add(dbUser);

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

	public static async Task<bool> RemoveParticipation(Event @event, User user)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var dbEvent = await context.Events
				.Include(e => e.Participants)
				.FirstOrDefaultAsync(e => e.Id == @event.Id);

			var dbUser = await context.Users.FindAsync(user.Id);

			if (dbEvent == null || dbUser == null)
				return false;

			dbEvent.Participants.Remove(dbUser);

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
