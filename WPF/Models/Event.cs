using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace WPF.Models;

public class Event
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Name { get; set; }
	public string Description { get; set; }
	public string ImageName { get; set; }
	public string ImagePath
	{
		get
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", ImageName);
		}
	}
	public DateTime Date { get; set; }
	public string Location { get; set; }

	public int OrganizerId { get; set; }
	public EventOrganizer Organizer { get; set; }

	public ObservableCollection<string> Categories { get; set; }
	public ObservableCollection<User> Participants { get; set; } = new ObservableCollection<User>();

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
