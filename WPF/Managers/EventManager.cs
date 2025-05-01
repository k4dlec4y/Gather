using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers
{
	public static class EventManager
	{
		public async static Task<ObservableCollection<Event>> GetEvents()
		{
			using var context = new AppDbContext();
			return new ObservableCollection<Event>(await context.Events
				.Include(e => e.Organizer)
				.Include(e => e.Participants)
				.ToListAsync());
		}

		public static async Task<bool> ContainsEvent(Event e)
		{
			using var context = new AppDbContext();
			return await context.Events.ContainsAsync(e);
		}

		public static async Task AddEvent(Event e)
		{
			using var context = new AppDbContext();
			context.Events.Add(e);
			await context.SaveChangesAsync();
		}

		public static async Task RemoveEvent(Event e)
		{
			using var context = new AppDbContext();
			context.Events.Remove(e);
			await context.SaveChangesAsync();
		}
	}
}