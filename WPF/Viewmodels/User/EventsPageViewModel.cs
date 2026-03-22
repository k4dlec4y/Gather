using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Managers;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.UserVM;

internal partial class EventsPageViewModel : ObservableObject
{
	private IWindowService _windowService { get; init; }

	public ObservableCollection<Event> Events { get; set; }

	[ObservableProperty]
	private Event? _selectedEvent;

	[ObservableProperty]
	private string _searchQuery = "";

	[ObservableProperty]
	private bool _isParticipatingOnly = false;

	[ObservableProperty]
	private MainViewModel _mainVM;

	public EventsPageViewModel(MainViewModel main, IWindowService windowService)
	{
		MainVM = main;
		_windowService = windowService;
		FilterEvents();
	}

	[RelayCommand]
	public void SelectEvent()
	{
		if (SelectedEvent != null)
			_windowService.ShowEventDetails(SelectedEvent, MainVM.CurrentUser.Friends);
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
		_windowService.SendEventInvitation(MainVM.CurrentUser, @event);
	}
}
