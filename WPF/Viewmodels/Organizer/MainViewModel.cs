using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using WPF.Models;
using WPF.Views.Organizer;

namespace WPF.Viewmodels.Organizer;

public partial class MainViewModel : ObservableObject
{
	[ObservableProperty]
	EventOrganizer _eventOrganizer;

	[ObservableProperty]
	Page _currentPage;

	public MainView MainWindow;

	public MainViewModel(EventOrganizer eventOrganizer, MainView mainWindow)
	{
		EventOrganizer = eventOrganizer;
		CurrentPage = new MyEventsPageView(this);
		MainWindow = mainWindow;
	}

	[RelayCommand]
	public void MyEvents()
	{
		CurrentPage = new MyEventsPageView(this);
	}

	[RelayCommand]
	public void Settings()
	{
		CurrentPage = new SettingsPageView(this);
	}
}
