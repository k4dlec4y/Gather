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

		public ObservableCollection<User> Participants { get; } = new();

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

		public bool AddParticipant(User participant)
		{
			if (Participants.Select(p => p.Username).Contains(participant.Username))
			{
				return false;
			}
			Participants.Add(participant);
			return true;
		}

		public bool DeleteParticipant(User participant)
		{
			return Participants.Remove(participant);
		}


		public bool ContainsParticipant(User participant)
		{
			return Participants.Any(p => p.Username == participant.Username);
		}
	}
}
