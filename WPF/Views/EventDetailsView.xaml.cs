using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using WPF.Models;

namespace WPF.Views;

public partial class EventDetailsView : Window
{
    public EventDetailsView(Event selectedEvent, ObservableCollection<User> friends)
    {
	    InitializeComponent();
		Debug.WriteLine($"Friends: {string.Join(", ", friends)}");
		DataContext = new Viewmodels.EventDetailsViewModel(selectedEvent, friends);
    }
}
