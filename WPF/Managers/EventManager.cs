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
			.Include(e => e.Organizer)
			.Include(e => e.Participants)
			.ToList());
	}

	public static async Task<bool> ContainsEvent(Event e)
	{
		using var context = new AppDbContext();
		return await context.Events.ContainsAsync(e);
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
			context.Events.Attach(e);
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