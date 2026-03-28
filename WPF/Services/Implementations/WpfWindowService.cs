using Microsoft.Win32;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WPF.Services.Implementations;

internal class WpfWindowService : IWindowService
{
	private Window _getLoginWindow()
	{
		var loginWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.Title.Equals("Login"));
		Debug.Assert(loginWindow != null, "Login window should not be null when getting it!");
		return loginWindow;
	}

	public void ShowLoginWindow()
	{
		var loginWindow = _getLoginWindow();
		loginWindow.Show();
		App.Current.MainWindow = loginWindow;
	}

	public void CloseMainWindow(string name)
	{
		foreach (Window window in Application.Current.Windows)
		{
			if (window != null && window.Title.Equals(name))
			{
				window.Close();
				break;
			}
		}
	}

	public void CloseAllWindows()
	{
		Application.Current.Shutdown();
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
		mainUserWindow.Owner = null;
		Application.Current.MainWindow = mainUserWindow;
		_getLoginWindow().Hide();
		mainUserWindow.Show();
	}

	public void OpenMainOrganizerWindow(EventOrganizer organizer)
	{
		var mainOrganizerWindow = new Views.Organizer.MainView(organizer);
		mainOrganizerWindow.Owner = null;
		Application.Current.MainWindow = mainOrganizerWindow;
		_getLoginWindow().Hide();
		mainOrganizerWindow.Show();
	}

	public void OpenMainAdminWindow(User admin)
	{
		var mainAdminWindow = new Views.Admin.MainView(admin);
		mainAdminWindow.Owner = null;
		Application.Current.MainWindow = mainAdminWindow;
		_getLoginWindow().Hide();
		mainAdminWindow.Show();
	}

	public void CreateEvent()
	{
		var window = new Views.Organizer.CreateEventWindowView();
		window.Owner = Application.Current.MainWindow;
		window.Show();
	}

	public void ShowEventDetails(Event selectedEvent, ObservableCollection<User> friends)
	{
		var detailWindow = new Views.EventDetailsView(selectedEvent, friends);
		detailWindow.Owner = Application.Current.MainWindow;
		detailWindow.Show();
	}

	public void EditEvent(Event selectedEvent)
	{
		var editWindow = new Views.Organizer.EditEventWindowView(selectedEvent);
		editWindow.Owner = Application.Current.MainWindow;
		editWindow.Show();
	}

	public void SendEventInvitation(Event selectedEvent)
	{
		var invitationWindow = new Views.UserV.SendInviteView(selectedEvent);
		invitationWindow.Owner = Application.Current.MainWindow;
		invitationWindow.Show();
	}

	public void SendBecomeOrganizerRequest()
	{
		var requestWindow = new Views.UserV.SendBecomeOrganizerRequestView();
		requestWindow.Owner = Application.Current.MainWindow;
		requestWindow.Show();
	}
}
