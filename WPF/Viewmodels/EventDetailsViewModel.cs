using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WPF.Models;

namespace WPF.Viewmodels;

public partial class EventDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private Event _event;

	[ObservableProperty]
	private string _weatherInfo;

	public ObservableCollection<Models.User> FriendsAttending { get; set; }

	public EventDetailsViewModel(Event @event, ObservableCollection<Models.User> friends)
    {
		Event = @event;
		FriendsAttending = new ObservableCollection<Models.User>(
			friends.Where(friend => friend.EventsToAttend.Any(e => e.Id == @event.Id))
		);
		OnPropertyChanged(nameof(FriendsAttending));
		LoadWeatherInfo();
	}

	private async void LoadWeatherInfo()
	{
		WeatherInfo = await WeatherAPI.WeatherFetcher.SetWeather(Event);
	}
}
