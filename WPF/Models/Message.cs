namespace WPF.Models
{
    public class Message
    {
		public User From { get; init; }
		public User To { get; init; }
		public string Content { get; init; }
		public bool seen { get; set; } = false;

		public Message(User from, User to, string content)
		{
			From = from;
			To = to;
			Content = content;
		}
	}
}
