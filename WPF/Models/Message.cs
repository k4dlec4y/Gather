using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WPF.Models
{
	public class Message
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public required int FromId { get; set; }
		public required User From { get; set; }

		public required int ToId { get; set; }
		public required User To { get; set; }

		public required string Content { get; set; }
		public required bool Seen { get; set; }
		public required DateTime SentTime { get; set; }
	}
}