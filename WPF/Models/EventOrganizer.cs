using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WPF.Models
{
	public class EventOrganizer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public required string Name { get; set; }
		public required byte[] PasswordHash { get; set; }
	}
}
