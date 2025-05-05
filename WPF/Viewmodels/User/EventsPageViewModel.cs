using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Managers;
using WPF.Models;
using WPF.Views.UserV;

namespace WPF.Viewmodels.UserVM;

public partial class EventsPageViewModel : ObservableObject
{
	public ObservableCollection<Event> Events { get; set; } = EventManager.GetEvents();

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
		MainVM = main;
	}

	[RelayCommand]
	public void SelectEvent()
	{
		if (SelectedEvent != null)
		{
			var detailWindow = new Views.EventDetailsView(SelectedEvent);
			detailWindow.Show();
		}
		SelectedEvent = null;
	}

	[RelayCommand]
	public void FilterEvents()
	{
		var filtered = (IsParticipatingOnly ? MainVM.CurrentUser.EventsToAttend : EventManager.GetEvents())
			.Where(e => (e.Name + e.Location + e.Description + string.Join(" ", e.Categories.ToList()))
				.Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		Events = new ObservableCollection<Event>(filtered);
		OnPropertyChanged(nameof(Events));
	}

	[RelayCommand]
	public void Participate(Event selectedEvent)
	{
		var currentUser = MainVM.CurrentUser;
		bool isCurrentlyParticipating = selectedEvent.ContainsParticipant(currentUser);

		if (isCurrentlyParticipating)
		{
			selectedEvent.DeleteParticipant(currentUser);
			MainVM.CurrentUser.EventsToAttend.Remove(selectedEvent);
		}
		else
		{
			selectedEvent.AddParticipant(currentUser);
			MainVM.CurrentUser.EventsToAttend.Add(selectedEvent);
		}
	}
}
