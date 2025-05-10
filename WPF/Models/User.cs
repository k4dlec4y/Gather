using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Models;

public class User
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Username { get; set; }
	public byte[] PasswordHash { get; set; }

	public ObservableCollection<Event> EventsToAttend { get; set; } = new ();
	public ObservableCollection<Message> Inbox { get; set; } = new ();
	public ObservableCollection<FriendRequest> FriendRequests { get; set; } = new();
	public ObservableCollection<Invite> Invites { get; set; } = new();
	public ObservableCollection<User> Friends { get; set; } = new ();

	public User(string username, byte[] hash)
	{
		Username = username;
		PasswordHash = hash;
	}

	protected User() { }  // for entity framework
}
