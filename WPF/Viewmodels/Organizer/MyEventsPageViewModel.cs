using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;

namespace WPF.Viewmodels.Organizer;

public partial class MyEventsPageViewModel : ObservableObject
{
	public ObservableCollection<Event> MyEvents { get; set; }

	[ObservableProperty]
	private Event? _selectedEvent;

	[ObservableProperty]
	private string _searchQuery = "";

	[ObservableProperty]
	private MainViewModel _mainVM;

	public MyEventsPageViewModel(MainViewModel mainVM)
	{
		MainVM = mainVM;
		MyEvents = Managers.EventManager.GetEventsOrganizerOrganize(MainVM.EventOrganizer);
	}

	[RelayCommand]
	public void SelectEvent()
	{
		if (SelectedEvent != null)
		{
			var detailWindow = new Views.EventDetailsView(SelectedEvent, []);
			detailWindow.Show();
		}
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
		var window = new Views.Organizer.CreateEventWindowView(MainVM.EventOrganizer, MyEvents);
		window.Show();
	}

	[RelayCommand]
	public void EditEvent(Event e)
	{
		var window = new Views.Organizer.EditEventWindowView(e, MainVM);
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
			MessageBox.Show("Failed to delete event. Please try again.");
		}
	}
}
