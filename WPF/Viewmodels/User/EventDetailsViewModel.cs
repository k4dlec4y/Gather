using CommunityToolkit.Mvvm.ComponentModel;
using WPF.Models;

namespace WPF.Viewmodels.UserVM
{
    public partial class EventDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private Event _event;

        public EventDetailsViewModel(Event @event)
        {
            Event = @event;
        }
    }
}
