using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Managers;
using WPF.Models;
using WPF.Views.UserV;

namespace WPF.Viewmodels.Admin;

public partial class EventsPageViewModel : ObservableObject
{
	public ObservableCollection<Event> Events { get; private set; } = EventManager.GetEvents();

	[ObservableProperty]
	private Event? _selectedEvent;

	[ObservableProperty]
	private string _searchQuery = "";

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

		Events = new ObservableCollection<Event>(filtered);
		OnPropertyChanged(nameof(Events));
	}

	[RelayCommand]
	public async Task DeleteEvent(Event e)
	{
		await EventManager.RemoveEvent(e);
	}
}
