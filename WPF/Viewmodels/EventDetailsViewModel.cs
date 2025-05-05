using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using WPF.Models;

namespace WPF.Viewmodels;

public partial class EventDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private Event _event;

    public EventDetailsViewModel(Event @event)
    {
        @event.ImageName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", @event.ImageName);
		Event = @event;
    }
}
