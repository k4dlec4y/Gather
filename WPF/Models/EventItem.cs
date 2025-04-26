using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Image = SixLabors.ImageSharp.Image;

namespace WPF.Models
{
	public class EventItem
	{
		public DateTime Date { get; set; }
		private string Location { get; set; }
		private ObservableCollection<string> Categories { get; set; }
		public string Description { get; set; }
		public string ImagePath { get; set; }
		public EventOrganizer Organizer { get; init; }
		public string Name { get; set; }

		public EventItem(
			DateTime date,
			string location,
			ObservableCollection<string> categories,
			string description,
			string image,
			EventOrganizer organizer,
			string name
		)
		{
			Date = date;
			Location = location;
			Categories = categories;
			Description = description;
			ImagePath = image;
			Organizer = organizer;
			Name = name;
		}
	}
}
