using Microsoft.EntityFrameworkCore;
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
				.Include(eo => eo.Events)
				.ThenInclude(e => e.Participants)
				.ToList();
		}
	}
	public static async Task<EventOrganizer?> GetEventOrganizerById(int id)
	{
		using (var db = new AppDbContext())
		{
			return await db.EventOrganizers.FirstOrDefaultAsync(eo => eo.Id == id);
		}
	}
	public static async Task<EventOrganizer?> GetEventOrganizerByUsername(string username)
	{
		using (var db = new AppDbContext())
		{
			return await db.EventOrganizers.FirstOrDefaultAsync(eo => eo.Username == username);
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
		catch
		{
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
		catch
		{
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
			db.EventOrganizers.Remove(eventOrganizer);
			db.Events.RemoveRange(eventOrganizer.Events);
			await db.SaveChangesAsync();
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
