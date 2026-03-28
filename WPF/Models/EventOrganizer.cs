using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Models;

public class EventOrganizer
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public required string Username { get; set; } = string.Empty;
	public required byte[] PasswordHash { get; set; } = Array.Empty<byte>();
	public required string Info { get; set; } = string.Empty;

	public ObservableCollection<Event> Events { get; set; } = new();
}
