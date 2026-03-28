using System.Collections.ObjectModel;
using WPF.Models;

namespace WPF.Services.Abstractions;

internal interface IWindowService
{
	public void ShowLoginWindow();
	public void CloseMainWindow(string name);
	public void CloseAllWindows();

	bool OpenFileDialog(string filter, out string filePath);

	void OpenMainUserWindow(User user);
	void OpenMainOrganizerWindow(EventOrganizer organizer);
	void OpenMainAdminWindow(User admin);

	void CreateEvent();
	void ShowEventDetails(Event selectedEvent, ObservableCollection<User> friends);
	void EditEvent(Event selectedEvent);

	void SendEventInvitation(Event selectedEvent);

	void SendBecomeOrganizerRequest();
}
