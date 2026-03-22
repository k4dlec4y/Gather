using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;
using WPF.Viewmodels.Organizer;

namespace WPF.Services.Abstractions;

internal interface IWindowService
{
	/**
	 * Closes all windows of the application except the login windows.
	 * Useful for example when signing out, so that the user is not left
	 * with open windows that are not relevant anymore.
	 */
	void CloseAllWindows();
	bool OpenFileDialog(string filter, out string filePath);

	void OpenMainUserWindow(User user);
	void OpenMainOrganizerWindow(EventOrganizer organizer);
	void OpenMainAdminWindow();

	void CreateEvent(EventOrganizer eventOrganizer, ObservableCollection<Event> myEvents, MainViewModel MainVM);
	void ShowEventDetails(Event selectedEvent, ObservableCollection<User> friends);
	void EditEvent(Event selectedEvent, MainViewModel MainVM);

	void SendEventInvitation(User user, Event selectedEvent);

	void SendBecomeOrganizerRequest(User currentUser);
}
