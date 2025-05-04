using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace WPF.Models
{
	public class EventOrganizer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public required string Username { get; set; }
		public required byte[] PasswordHash { get; set; }
		public required string Info { get; set; }

		public ObservableCollection<Event> OrganizedEvents { get; set; } = new();
	}
}
