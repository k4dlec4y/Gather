using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Admin;

internal partial class EventsPageViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }

	[ObservableProperty]
	private Event? _selectedEvent;
	[ObservableProperty]
	private string _searchQuery = "";

	public ObservableCollection<Event> Events { get; private set; } =
		Managers.EventManager.GetEvents();

	public EventsPageViewModel(
		IDialogService dialogService,
		IWindowService windowService
	) {
		_dialogService = dialogService;
		_windowService = windowService;
	}

	[RelayCommand]
	public void SelectEvent()
	{
		if (SelectedEvent != null)
			_windowService.ShowEventDetails(SelectedEvent, []);
		SelectedEvent = null;
	}

	[RelayCommand]
	public void FilterEvents()
	{
		var filtered = Managers.EventManager.GetEvents()
			.Where(e => (e.Name +
						 e.Location +
						 e.Description +
						 string.Join(" ", e.Categories.ToList())
						).Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		Events = new ObservableCollection<Event>(filtered);
		OnPropertyChanged(nameof(Events));
	}

	[RelayCommand]
	public async Task DeleteEvent(Event e)
	{
		if (e == null)
			return;

		if (await Managers.EventManager.RemoveEvent(e))
		{
			Events.Remove(e);
			OnPropertyChanged(nameof(Events));

			if (SelectedEvent == e)
				SelectedEvent = null;
		}
		else
		{
			_dialogService.ShowError("Failed to delete event. Please try again.");
		}
	}
}
