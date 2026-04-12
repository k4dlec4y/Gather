using WPF.Models;

namespace WPF.Services.Abstractions;

public interface IOrganizerIdentityService
{
	EventOrganizer? CurrentEventOrganizer { get; }
	bool IsLoggedIn => CurrentEventOrganizer != null;

	void Login(EventOrganizer eventOrganizer);
	void Logout();
}