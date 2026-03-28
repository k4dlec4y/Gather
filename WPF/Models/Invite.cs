using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Models;

public class Invite
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int EventId;
	public Event Event { get; set; } = null!;

	public int FromId { get; set; }
	public User From { get; set; } = null!;

	public int ToId { get; set; }
	public User To { get; set; } = null!;

	public required string Content { get; set; } = string.Empty;
}
