using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class User
    {
        public string Name { get; set; }
		public int Age { get; set; }
		public string PasswordHash{ get; set; }

		public List<EventItem> EventsToAttend { get; set; }
		public List<Invitation> Invitations { get; set; }

		public List<User> Friends { get; set; }
		public List<FriendRequest> FriendRequests { get; set; }

		public User(string name, int age, string hash)
        {
            Name = name;
            Age = age;
            PasswordHash = hash;
			Friends = new List<User>();
            Invitations = new List<Invitation>();
            EventsToAttend = new List<EventItem>();
            FriendRequests = new List<FriendRequest>();
		}
    }
}
