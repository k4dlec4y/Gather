using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

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
	public byte[] ImageData { get; set; }
	public ImageSource GetImageSource
	{
		get
		{
			var bitmap = new BitmapImage();
			using (var stream = new MemoryStream(ImageData))
			{
				bitmap.BeginInit();
				bitmap.CacheOption = BitmapCacheOption.OnLoad;
				bitmap.StreamSource = stream;
				bitmap.EndInit();
				bitmap.Freeze();
			}
			return bitmap;
		}
	}

	public int OrganizerId { get; set; }
	public EventOrganizer Organizer { get; set; }

	public ObservableCollection<string> Categories { get; set; }
	public ObservableCollection<User> Participants { get; set; } = new ObservableCollection<User>();

	[NotMapped]
	public bool IsCurrentUserParticipating { get; set; }

	public Event(
		string name,
		string description,
		DateTime date,
		string location,
		byte[] imageData,
		int organizerId,
		ObservableCollection<string> categories
	)
	{
		Name = name;
		Description = description;
		Date = date;
		Location = location;
		ImageData = imageData;
		OrganizerId = organizerId;
		Categories = categories;
	}

	protected Event() { }  // for entity framework

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
