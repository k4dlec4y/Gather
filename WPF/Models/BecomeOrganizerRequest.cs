using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Models;

public class BecomeOrganizerRequest
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string RequestText { get; set; }
	public int UserId { get; set; }
	public User User { get; set; }
}
