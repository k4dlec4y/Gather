using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Managers;
using WPF.Models;
using WPF.Views.UserV;

namespace WPF.Viewmodels.UserVM
{
    public partial class EventsPageViewModel : ObservableObject
    {
		public ObservableCollection<Event> Events { get; set; }

		[ObservableProperty]
		private Event? _selectedEvent;

		[ObservableProperty]
		private string _searchQuery = "";

		[ObservableProperty]
		private bool _isParticipatingOnly = false;

		[ObservableProperty]
		private MainViewModel _mainVM;

		public EventsPageViewModel(MainViewModel main)
        {
            Events = EventManager.GetEvents();
			MainVM = main;
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

			/*if (isParticipatingOnly)
			{
				filtered = filtered.Where(e => e.ContainsParticipant());
			}*/

			Events = new ObservableCollection<Event>(filtered);
			OnPropertyChanged(nameof(Events));
		}
	}
}
