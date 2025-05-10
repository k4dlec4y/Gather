using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WPF.Models;

namespace WPF.Viewmodels;

public partial class EventDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private Event _event;

	public ObservableCollection<User> FriendsAttending { get; set; }

	public EventDetailsViewModel(Event @event, ObservableCollection<User> friends)
    {
		Event = @event;
		FriendsAttending = new ObservableCollection<User>(friends.Where(friend => friend.EventsToAttend.Any(e => e.Id == @event.Id)));
		OnPropertyChanged(nameof(FriendsAttending));
	}
}
