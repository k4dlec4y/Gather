using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using WPF.Models;

namespace WPF.Viewmodels;

public partial class EventDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private Event _event;

	public ObservableCollection<User> FriendsAttending { get; set; }

	public EventDetailsViewModel(Event @event, ObservableCollection<User> friends)
    {
        @event.ImageName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", @event.ImageName);
		Event = @event;
		FriendsAttending = new ObservableCollection<User>(friends.Where(friend => friend.EventsToAttend.Any(e => e.Id == @event.Id)));
		Debug.WriteLine($"Friends: {string.Join(", ", FriendsAttending.Select(f => f.Username))}");
		OnPropertyChanged(nameof(FriendsAttending));
	}
}
