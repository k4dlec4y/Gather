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
	public static async Task UpdateEventOrganizer(EventOrganizer eventOrganizer)
	{
		using var db = new AppDbContext();
		using var transaction = await db.Database.BeginTransactionAsync();
		try
		{
			db.EventOrganizers.Update(eventOrganizer);
			db.SaveChanges();
		}
		catch
		{
			await transaction.RollbackAsync();
		}
	}
	public static async Task DeleteEventOrganizer(EventOrganizer eventOrganizer)
	{
		using var db = new AppDbContext();
		using var transaction = await db.Database.BeginTransactionAsync();
		try
		{
			db.EventOrganizers.Remove(eventOrganizer);
			db.SaveChanges();
		}
		catch
		{
			await transaction.RollbackAsync();
		}
	}
}
