using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace WPF.Models
{
	public static class EventManagerItem
	{

		private static ObservableCollection<EventItem> Events =
		[
			new EventItem(new DateTime(2025, 12, 4, 3, 0, 0), "Brno", ["SPORT"], "sport activity with family", new Uri(Path.GetFullPath(Path.Combine("Data", "event_image.jpg"))).AbsoluteUri, new EventOrganizer("Jano", 62, "dasd"), "Olympiada"),
			new EventItem(new DateTime(2025, 4, 6, 12, 0, 0), "Trencin", ["Family, Music"], "concert v trencine, prid", new Uri(Path.GetFullPath(Path.Combine("Data", "event_image.jpg"))).AbsoluteUri, new EventOrganizer("Jano", 62, "dasd"), "Garaz")
		];

		public static ObservableCollection<EventItem> GetEvents() => Events;

		public static void AddEvent(EventItem e) => Events.Add(e);
	}
}