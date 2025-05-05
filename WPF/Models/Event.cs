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

	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public DateTime Date { get; set; }
	public string Location { get; set; } = string.Empty;
	public string ImageName { get; set; } = string.Empty;
	public string ImagePath
	{
		get
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", ImageName);
		}
	}

	public int OrganizerId { get; set; }
	public EventOrganizer Organizer { get; set; }

	public ObservableCollection<string> Categories { get; set; }
	public ObservableCollection<User> Participants { get; set; } = new ObservableCollection<User>();

	[NotMapped]
	public bool IsCurrentUserParticipating { get; set; }

	public async Task<bool> AddParticipant(User participant)
	{
		if (Participants.Select(p => p.Username).Contains(participant.Username))
		{
			return false;
		}

		return await Managers.ParticipationManager.AddParticipation(this, participant);
	}

	public async Task<bool> DeleteParticipant(User participant)
	{
		return await Managers.ParticipationManager.RemoveParticipation(this, participant);
	}

	public bool ContainsParticipant(User participant)
	{
		return Participants.Any(p => p.Username == participant.Username);
	}
}
