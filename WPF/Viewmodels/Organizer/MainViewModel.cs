using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using WPF.Models;

namespace WPF.Viewmodels.Organizer;

public partial class MainViewModel : ObservableObject
{
	[ObservableProperty]
	EventOrganizer _eventOrganizer;

	[ObservableProperty]
	Page _currentPage;

	public MainViewModel(EventOrganizer eventOrganizer)
	{
		EventOrganizer = eventOrganizer;
		CurrentPage = new Views.Organizer.MyEventsPageView(this);
	}

	[RelayCommand]
	public void MyEvents()
	{
		CurrentPage = new Views.Organizer.MyEventsPageView(this);
	}

	[RelayCommand]
	public void Settings()
	{
		CurrentPage = new Views.Organizer.SettingsPageView(this);
	}
}
