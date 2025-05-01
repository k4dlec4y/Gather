using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Models
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Username { get; set; }
		public byte[] PasswordHash { get; set; }

		public ICollection<Event> EventsToAttend { get; set; } = new ObservableCollection<Event>();
		public ICollection<Message> Messages { get; set; } = new List<Message>();
		public ICollection<User> Friends { get; set; } = new List<User>();

		public User(string username, byte[] hash)
		{
			Username = username;
			PasswordHash = hash;
		}

		protected User() { }
	}
}
