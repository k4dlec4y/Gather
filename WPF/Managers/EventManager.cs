using System.Collections.ObjectModel;
using System.IO;
using WPF.Models;

namespace WPF.Managers
{
	public static class EventManager
	{
		private static ObservableCollection<Event> Events =
		[
			new Event(new DateTime(2025, 12, 4, 3, 0, 0), "Brno", ["SPORT"], "sport activity with family", new Uri(Path.GetFullPath(Path.Combine("Data", "event_image.jpg"))).AbsoluteUri, new EventOrganizer("Jano", System.Text.Encoding.UTF8.GetBytes("dasd")), "Olympiada"),
			new Event(new DateTime(2025, 4, 6, 12, 0, 0), "Trencin", ["Family, Music"], "concert v trencine, prid", new Uri(Path.GetFullPath(Path.Combine("Data", "event_image.jpg"))).AbsoluteUri, new EventOrganizer("Jano", System.Text.Encoding.UTF8.GetBytes("dasd")), "Garaz")
		];

		public static ObservableCollection<Event> GetEvents() => Events;

		public static void AddEvent(Event e) => Events.Add(e);
	}
}