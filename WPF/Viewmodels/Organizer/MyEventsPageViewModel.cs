using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class MyEventsPageViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }

	public ObservableCollection<Event> MyEvents { get; set; }

	[ObservableProperty]
	private Event? _selectedEvent;

	[ObservableProperty]
	private string _searchQuery = "";

	[ObservableProperty]
	private MainViewModel _mainVM;

	public MyEventsPageViewModel(
		MainViewModel mainVM,
		IDialogService dialogService,
		IWindowService windowService
	) {
		_mainVM = mainVM;
		_dialogService = dialogService;
		_windowService = windowService;

		MyEvents = Managers.EventManager.GetEventsOrganizerOrganize(_mainVM.EventOrganizer);
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
		var filtered = Managers.EventManager.GetEventsOrganizerOrganize(MainVM.EventOrganizer)
			.Where(e => (e.Name + e.Location + e.Description + string.Join(" ", e.Categories.ToList()))
				.Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		MyEvents = new ObservableCollection<Event>(filtered);
		OnPropertyChanged(nameof(MyEvents));
	}

	[RelayCommand]
	public void CreateEvent()
	{
		_windowService.CreateEvent(MainVM.EventOrganizer, MyEvents, MainVM);
	}

	[RelayCommand]
	public void EditEvent(Event e)
	{
		var window = new Views.Organizer.EditEventWindowView(e, MainVM);
		window.Owner = MainVM.MainWindow;
		window.Show();
	}

	[RelayCommand]
	public async Task DeleteEvent(Event e)
	{
		if (e == null) return;

		if (await Managers.EventManager.RemoveEvent(e))
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
