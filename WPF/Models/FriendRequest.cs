using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Models;

public class FriendRequest
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public required int FromId { get; set; }
	public required User From { get; set; }

	public required int ToId { get; set; }
	public required User To { get; set; }

	public required string Content { get; set; }
}
