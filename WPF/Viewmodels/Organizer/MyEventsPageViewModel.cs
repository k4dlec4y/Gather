using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WPF.Managers;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class MyEventsPageViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }

	private EventOrganizer _currentEventOrganizer { get; init; }

	public ObservableCollection<Event> MyEvents { get; set; }

	[ObservableProperty]
	private Event? _selectedEvent;

	[ObservableProperty]
	private string _searchQuery = "";

	public MyEventsPageViewModel(
		IOrganizerIdentityService organizerIdentityService,
		IDialogService dialogService,
		IWindowService windowService)
	{
		Debug.Assert(
			organizerIdentityService.CurrentEventOrganizer != null,
			"CurrentEventOrganizer should not be null when initializing MyEventsPageViewModel");
		_currentEventOrganizer = organizerIdentityService.CurrentEventOrganizer!;
		MyEvents = _currentEventOrganizer.Events;

		_dialogService = dialogService;
		_windowService = windowService;
	}

	[RelayCommand]
	public void SelectEvent()
	{
		if (SelectedEvent != null)
		{
			_windowService.ShowEventDetails(SelectedEvent, new ObservableCollection<Models.User>());
		}
		SelectedEvent = null;
	}

	[RelayCommand]
	public void FilterEvents()
	{
		var filtered = EventManager.GetEventsOrganizerOrganize(_currentEventOrganizer)
			.Where(e => (e.Name + e.Location + e.Description + string.Join(" ", e.Categories.ToList()))
				.Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		MyEvents = new ObservableCollection<Event>(filtered);
		OnPropertyChanged(nameof(MyEvents));
	}

	[RelayCommand]
	public void CreateEvent()
	{
		_windowService.CreateEvent();
	}

	[RelayCommand]
	public void EditEvent(Event e)
	{
		_windowService.EditEvent(e);
	}

	[RelayCommand]
	public async Task DeleteEvent(Event e)
	{
		if (e == null)
		{
			return;
		}

		if (await EventManager.RemoveEvent(e))
		{
			MyEvents.Remove(e);
			if (SelectedEvent == e)
			{
				SelectedEvent = null;
			}
		}
		else
		{
			_dialogService.ShowError("Failed to delete event. Please try again.");
		}
	}
}
