using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;
using WPF.Viewmodels.Organizer;

namespace WPF.Services.Implementations;

internal class WpfWindowService : IWindowService
{
	public void CloseAllWindows()
	{
		foreach (Window window in Application.Current.Windows)
		{
			if (window == null || window.Name.Equals("Login"))
				continue;

			window.Close();
		}
	}

	public bool OpenFileDialog(string filter, out string filePath)
	{
		var openFileDialog = new OpenFileDialog{Filter = filter};
		if (openFileDialog.ShowDialog() == true)
		{
			filePath = openFileDialog.FileName;
			return true;
		}
		else
		{
			filePath = "";
			return false;
		}
	}

	public void OpenMainUserWindow(User user)
	{
		var mainUserWindow = new Views.UserV.MainView(user);
		mainUserWindow.Owner = Application.Current.MainWindow;
		Application.Current.MainWindow = mainUserWindow;
		mainUserWindow.Show();
	}

	public void OpenMainOrganizerWindow(EventOrganizer organizer)
	{
		var mainOrganizerWindow = new Views.Organizer.MainView(organizer);
		mainOrganizerWindow.Owner = Application.Current.MainWindow;
		Application.Current.MainWindow = mainOrganizerWindow;
		mainOrganizerWindow.Show();
	}

	public void OpenMainAdminWindow()
	{
		var mainAdminWindow = new Views.Admin.MainView();
		mainAdminWindow.Owner = Application.Current.MainWindow;
		Application.Current.MainWindow = mainAdminWindow;
		mainAdminWindow.Show();
	}

	public void CreateEvent(EventOrganizer eventOrganizer, ObservableCollection<Event> myEvents, MainViewModel MainVM)
	{
		var window = new Views.Organizer.CreateEventWindowView(eventOrganizer, myEvents);
		window.Owner = Application.Current.MainWindow;
		window.Show();
	}

	public void ShowEventDetails(Event selectedEvent, ObservableCollection<User> friends)
	{
		var detailWindow = new Views.EventDetailsView(selectedEvent, friends);
		detailWindow.Owner = Application.Current.MainWindow;
		detailWindow.Show();
	}

	public void EditEvent(Event selectedEvent, MainViewModel MainVM)
	{
		var editWindow = new Views.Organizer.EditEventWindowView(selectedEvent, MainVM);
		editWindow.Owner = Application.Current.MainWindow;
		editWindow.Show();
	}

	public void SendEventInvitation(User user, Event selectedEvent)
	{
		var invitationWindow = new Views.UserV.SendInviteView(user, selectedEvent);
		invitationWindow.Owner = Application.Current.MainWindow;
		invitationWindow.Show();
	}

	public void SendBecomeOrganizerRequest(User currentUser)
	{
		var requestWindow = new Views.UserV.SendBecomeOrganizerRequestView(currentUser);
		requestWindow.Owner = Application.Current.MainWindow;
		requestWindow.Show();
	}
}
