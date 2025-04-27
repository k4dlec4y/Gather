using System.Collections.ObjectModel;
namespace WPF.Models
{
	public class Event
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImagePath { get; set; }

		public DateTime Date { get; set; }
		public string Location { get; set; }

		public EventOrganizer Organizer { get; init; }

		public ObservableCollection<string> Categories { get; init; }

		public Event(
			DateTime date,
			string location,
			ObservableCollection<string> categories,
			string description,
			string image,
			EventOrganizer organizer,
			string name)
		{
			Date = date;
			Location = location;
			Categories = categories;
			Description = description;
			ImagePath = image;
			Organizer = organizer;
			Name = name;
		}
	}
}
