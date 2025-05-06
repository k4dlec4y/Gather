using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class EventOrganizerManager
{
	public static List<EventOrganizer> GetEventOrganizers()
	{
		using (var db = new AppDbContext())
		{
			return db.EventOrganizers
				.AsNoTracking()
				.Include(eo => eo.Events)
					.ThenInclude(e => e.Participants)
				.ToList();
		}
	}

	public static async Task<EventOrganizer?> GetEventOrganizerById(int id)
	{
		using (var db = new AppDbContext())
		{
			return await db.EventOrganizers
				.AsNoTracking()
				.Include(eo => eo.Events)
					.ThenInclude(e => e.Participants)
				.FirstOrDefaultAsync(eo => eo.Id == id);
		}
	}

	public static async Task<EventOrganizer?> GetEventOrganizerByUsername(string username)
	{
		using (var db = new AppDbContext())
		{
			return await db.EventOrganizers
				.AsNoTracking()
				.Include(eo => eo.Events)
					.ThenInclude(e => e.Participants)
				.FirstOrDefaultAsync(eo => eo.Username == username);
		}
	}

	public static async Task AddEventOrganizer(EventOrganizer eventOrganizer)
	{
		using var db = new AppDbContext();
		using var transaction = await db.Database.BeginTransactionAsync();
		try
		{
			await db.EventOrganizers.AddAsync(eventOrganizer);
			await db.SaveChangesAsync();
			await transaction.CommitAsync();
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			Debug.WriteLine(ex.InnerException?.Message);
			await transaction.RollbackAsync();
		}
	}

	public static async Task<bool> UpdateEventOrganizer(EventOrganizer eventOrganizer)
	{
		using var db = new AppDbContext();
		using var transaction = await db.Database.BeginTransactionAsync();
		try
		{
			db.EventOrganizers.Update(eventOrganizer);
			await db.SaveChangesAsync();
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

	public static async Task<bool> DeleteEventOrganizer(EventOrganizer eventOrganizer)
	{
		using var db = new AppDbContext();
		using var transaction = await db.Database.BeginTransactionAsync();
		try
		{
			var organizerInDb = await db.EventOrganizers
			.Include(o => o.Events)
			.FirstOrDefaultAsync(o => o.Id == eventOrganizer.Id);

			if (organizerInDb == null)
			{
				Debug.WriteLine("Event organizer not found.");
				return false;
			}

			db.Events.RemoveRange(organizerInDb.Events);

			db.EventOrganizers.Remove(organizerInDb);
			await db.SaveChangesAsync();
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
