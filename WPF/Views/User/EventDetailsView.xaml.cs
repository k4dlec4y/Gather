using System.Windows;
using WPF.Models;

namespace WPF.Views.UserV;

public partial class EventDetailsView : Window
{
    public EventDetailsView(Event selectedEvent)
    {
	    InitializeComponent();
	    DataContext = new Viewmodels.UserVM.EventDetailsViewModel(selectedEvent);
    }
}
