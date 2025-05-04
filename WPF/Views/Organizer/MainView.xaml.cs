using System.Windows;
using WPF.Models;

namespace WPF.Views.Organizer;

public partial class MainView : Window
{
	public MainView(EventOrganizer eventOrganizer)
    {
        InitializeComponent();
		DataContext = new Viewmodels.Organizer.MainViewModel(eventOrganizer);
	}
}
