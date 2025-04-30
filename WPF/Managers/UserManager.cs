using System.Collections.ObjectModel;
using WPF.Models;

namespace WPF.Managers
{
    public static class UserManager
    {
        private static ObservableCollection<User> _users { get; set; } =
        [
            new User("Matus", System.Text.Encoding.UTF8.GetBytes("dasd")),
		];

        public static ObservableCollection<User> GetUsers() => _users;

        public static void AddUser(User user) => _users.Add(user);

		public static bool DeleteUser(User user) => _users.Remove(user);

        public static bool ContainsUsername(string username) 
            => _users.Any(u => u.Username == username);

        public static (bool, User) GetUser(string username)
        {
			var user = _users.Where(u => u.Username == username);
            if (user.Count() == 0)
            {
				return (false, new User("", []));
			}
            return (true, user.ElementAt(0));
        }
	}
}
