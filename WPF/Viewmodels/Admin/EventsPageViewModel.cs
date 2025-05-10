using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;

namespace WPF.Viewmodels.Admin;

public partial class EventsPageViewModel : ObservableObject
{
	public ObservableCollection<Event> Events { get; private set; } =
		Managers.EventManager.GetEvents();

	[ObservableProperty]
	private Event? _selectedEvent;

	[ObservableProperty]
	private string _searchQuery = "";

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
		var filtered = Managers.EventManager.GetEvents()
			.Where(e => (e.Name + e.Location + e.Description + string.Join(" ", e.Categories.ToList()))
				.Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		Events = new ObservableCollection<Event>(filtered);
		OnPropertyChanged(nameof(Events));
	}

	[RelayCommand]
	public async Task DeleteEvent(Event e)
	{
		if (e == null) return;

		if (await Managers.EventManager.RemoveEvent(e))
		{
			Events.Remove(e);
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
