using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WPF.Models
{
	public class Event
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public required string Name { get; set; }
		public required string Description { get; set; }
		public required string ImagePath { get; set; }
		public DateTime Date { get; set; }
		public required string Location { get; set; }

		public int OrganizerId { get; set; }
		public virtual required EventOrganizer Organizer { get; set; }

		public virtual ICollection<string> Categories { get; set; } = new ObservableCollection<string>();
		public virtual ICollection<User> Participants { get; set; } = new ObservableCollection<User>();

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
