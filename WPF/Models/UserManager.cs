using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public static class UserManager
    {
        private static ObservableCollection<User> Users { get; set; } =
        [
            new User("Matus", 24, "dasd"),
			new User("Pavol", 18, "dasd"),
			new User("Zdenek", 18, "dasd"),
		];

        public static ObservableCollection<User> GetUsers() => Users;

        public static void addUser(User user) => Users.Add(user);

		public static void deleteUser(User user) => Users.Remove(user);
	}
}
