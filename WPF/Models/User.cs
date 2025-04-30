namespace WPF.Models
{
    public class User
    {
        public string Username { get; set; }
		public DateOnly BirthDate { get; set; }
		public byte[] PasswordHash{ get; set; }

		public List<Event> EventsToAttend { get; set; } = new List<Event>();
        public List<Message> Messages { get; set; } = new List<Message>();

        public List<User> Friends { get; set; } = new List<User>();

		public User(string username, byte[] hash)
        {
            Username = username;
            PasswordHash = hash;
		}
    }
}
