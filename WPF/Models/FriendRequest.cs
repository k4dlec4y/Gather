namespace WPF.Models
{
	public class FriendRequest : Message
	{
		public FriendRequest(User user, User to, string content) : base(user, to, content) { }
	}
}
