using CommunityToolkit.Mvvm.ComponentModel;
using WPF.Models;
using WPF.Services.Abstractions;

public partial class OrganizerIdentityService : ObservableObject, IOrganizerIdentityService
{
	[ObservableProperty]
	private EventOrganizer? _currentEventOrganizer;

	public void Login(EventOrganizer eventOrganizer) => CurrentEventOrganizer = eventOrganizer;
	public void Logout() => CurrentEventOrganizer = null;
}	