using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Models
{
    public class Invite
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int EventId;
		public required Event Event { get; set; }

		public int FromId { get; set; }
		public required User From { get; set; }

		public int ToId { get; set; }
		public required User To { get; set; }

		public required string Content { get; set; }
	}
}
