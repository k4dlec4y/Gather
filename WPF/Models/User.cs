namespace WPF.Models
{
    public class User
    {
        public string Name { get; set; }
		public int Age { get; set; }
		public string PasswordHash{ get; set; }

		public List<Event> EventsToAttend { get; set; } = new List<Event>();
        public List<Message> Messages { get; set; } = new List<Message>();

        public List<User> Friends { get; set; } = new List<User>();

		public User(string name, int age, string hash)
        {
            Name = name;
            Age = age;
            PasswordHash = hash;
		}
    }
}
