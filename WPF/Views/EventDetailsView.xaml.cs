using System.Windows;
using WPF.Models;

namespace WPF.Views;

public partial class EventDetailsView : Window
{
    public EventDetailsView(Event selectedEvent)
    {
	    InitializeComponent();
	    DataContext = new Viewmodels.EventDetailsViewModel(selectedEvent);
    }
}
