using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.Viewmodels
{
    class HomeViewModel
    {
		public ObservableCollection<EventItem> Events { get; set; }

		public HomeViewModel()
		{
			Events = EventManagerItem.GetEvents();
		}
	}
}
