using System.Collections.ObjectModel;
using WPF.Models;
using WPF.Managers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Views.User;

namespace WPF.Viewmodels.User
{
    public partial class EventsPageViewModel : ObservableObject
    {
		public ObservableCollection<Event> Events { get; set; }

		[ObservableProperty]
		private Event? selectedEvent;

		[ObservableProperty]
		private string searchQuery = "";

		public EventsPageViewModel()
        {
            Events = EventManager.GetEvents();
		}

		[RelayCommand]
		public void SelectEvent()
		{
			if (SelectedEvent != null)
			{
				var detailWindow = new EventDetailsView(SelectedEvent);
				detailWindow.Show();
			}
		}

		[RelayCommand]
		public void FilterEvents()
		{
			var filtered = EventManager.GetEvents()
				.Where(e => (e.Name + e.Location + e.Description + string.Join(" ", e.Categories.ToList()))
					.Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase))
				.ToList();

			Events = new ObservableCollection<Event>(filtered);
			OnPropertyChanged(nameof(Events));
		}
	}
}
