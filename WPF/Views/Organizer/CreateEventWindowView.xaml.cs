using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;

namespace WPF.Views.Organizer;

public partial class CreateEventWindowView : Window
{
    public CreateEventWindowView(EventOrganizer eventOrganizer, ObservableCollection<Event> myEvents)
    {
        InitializeComponent();
        DataContext = new Viewmodels.Organizer.CreateEventWindowViewModel(eventOrganizer, myEvents);
	}
}
