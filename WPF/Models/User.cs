namespace WPF.Models
{
    public class User
    {
        public string Username { get; set; }
		public int Age { get; set; }
		public string PasswordHash{ get; set; }

		public List<Event> EventsToAttend { get; set; } = new List<Event>();
        public List<Message> Messages { get; set; } = new List<Message>();

        public List<User> Friends { get; set; } = new List<User>();

		public User(string username, int age, string hash)
        {
            Username = username;
            Age = age;
            PasswordHash = hash;
		}
    }
}
