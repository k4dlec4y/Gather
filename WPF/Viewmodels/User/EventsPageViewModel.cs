using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WPF.Managers;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.User;

internal partial class EventsPageViewModel : ObservableObject
{
	private IWindowService _windowService { get; init; }
	private Models.User _currentUser { get; init; }

	public ObservableCollection<Event> Events { get; set; } = new();

	[ObservableProperty]
	private Event? _selectedEvent = null;

	[ObservableProperty]
	private string _searchQuery = "";

	[ObservableProperty]
	private bool _isParticipatingOnly = false;

	public EventsPageViewModel(
		IUserIdentityService userIdentityService,
		IWindowService windowService
	) {
		Debug.Assert(userIdentityService.CurrentUser != null, "CurrentUser should not be null when initializing EventsPageViewModel");
		_currentUser = userIdentityService.CurrentUser;
		_windowService = windowService;
		FilterEvents();
	}

	[RelayCommand]
	public void SelectEvent()
	{
		if (SelectedEvent != null)
			_windowService.ShowEventDetails(SelectedEvent, _currentUser.Friends);
		SelectedEvent = null;
	}

	[RelayCommand]
	public void FilterEvents()
	{
		var filtered = (IsParticipatingOnly ? 
			EventManager.GetEventsUserAttend(_currentUser) : 
			EventManager.GetEvents(_currentUser))
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
			await ParticipationManager.RemoveParticipation(@event, _currentUser);
		}
		else
		{
			await ParticipationManager.AddParticipation(@event, _currentUser);
		}
		FilterEvents();
	}

	[RelayCommand]
	public void SendInvite(Event @event)
	{
		_windowService.SendEventInvitation(@event);
	}
}
