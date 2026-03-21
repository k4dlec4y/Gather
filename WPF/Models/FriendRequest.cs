using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Models;

public class FriendRequest
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int FromId { get; set; }
	public User From { get; set; }

	public int ToId { get; set; }
	public User To { get; set; }

	public string Content { get; set; }
}
