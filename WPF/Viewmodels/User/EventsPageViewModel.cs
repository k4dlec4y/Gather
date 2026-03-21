using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Managers;
using WPF.Models;

namespace WPF.Viewmodels.UserVM;

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
		MainVM = main;
		FilterEvents();
	}

	[RelayCommand]
	public void SelectEvent()
	{
		if (SelectedEvent != null)
		{
			var detailWindow = new Views.EventDetailsView(SelectedEvent, MainVM.CurrentUser.Friends);
			detailWindow.Owner = MainVM.MainWindow;
			detailWindow.Show();
		}
		SelectedEvent = null;
	}

	[RelayCommand]
	public void FilterEvents()
	{
		var filtered = (IsParticipatingOnly ? 
			EventManager.GetEventsUserAttend(MainVM.CurrentUser) : 
			EventManager.GetEvents(MainVM.CurrentUser))
			.Where(e => (e.Name + e.Location + e.Description + string.Join(" ", e.Categories.ToList()))
				.Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		Events = new ObservableCollection<Event>(filtered);
		OnPropertyChanged(nameof(Events));
	}

	[RelayCommand]
	public async Task Participate(Event @event)
	{
		if (@event.IsCurrentUserParticipating)
		{
			await ParticipationManager.RemoveParticipation(@event, MainVM.CurrentUser);
		}
		else
		{
			await ParticipationManager.AddParticipation(@event, MainVM.CurrentUser);
		}
		FilterEvents();
	}

	[RelayCommand]
	public void SendInvite(Event @event)
	{
		var window = new Views.UserV.SendInviteView(MainVM, @event);
		window.Owner = MainVM.MainWindow;
		window.Show();
	}
}
