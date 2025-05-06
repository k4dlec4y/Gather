using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class EventManager
{
	public static ObservableCollection<Event> GetEvents()
	{
		using var context = new AppDbContext();
		return new ObservableCollection<Event>(context.Events
			.AsNoTracking()
			.Include(e => e.Organizer)
			.Include(e => e.Participants)
			.ToList());
	}

	public static ObservableCollection<Event> GetEvents(User user)
	{
		using var context = new AppDbContext();
		var events = context.Events
			.AsNoTracking()
			.Include(e => e.Organizer)
			.Include(e => e.Participants)
			.ToList();

		foreach (var ev in events)
		{
			ev.IsCurrentUserParticipating = ev.Participants.Any(p => p.Id == user.Id);
		}

		return new ObservableCollection<Event>(events);
	}

	public static ObservableCollection<Event> GetEventsUserAttend(User user)
	{
		using var context = new AppDbContext();
		var events = context.Events
			.AsNoTracking()
			.Include(e => e.Organizer)
			.Include(e => e.Participants)
			.Where(e => e.Participants.Any(p => p.Id == user.Id))
			.ToList();

		foreach (var ev in events)
		{
			ev.IsCurrentUserParticipating = true;
		}

		return new ObservableCollection<Event>(events);
	}

	public static async Task<bool> ContainsEvent(Event e)
	{
		using var context = new AppDbContext();
		return await context.Events
			.AsNoTracking()
			.AnyAsync(ev => ev.Id == e.Id);
	}

	public static async Task<bool> CreateEvent(Event e)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			context.Events.Add(e);
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

	public static async Task<bool> RemoveEvent(Event e)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			context.TrackEntity(e);
			context.Events.Remove(e);

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