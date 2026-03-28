using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class MainViewModel : ObservableObject
{
	public IOrganizerIdentityService OrganizerIdentityService { get; init; }
	public INavigationService Navigation { get; init; }
	private IWindowService _windowService { get; init; }

	public MainViewModel(
		IWindowService windowService,
		IOrganizerIdentityService organizerIdentityService,
		INavigationService navigation)
	{
		Debug.Assert(
			organizerIdentityService.CurrentEventOrganizer != null,
			"Current event organizer cannot be null when initializing MainViewModel");
		OrganizerIdentityService = organizerIdentityService;

		Navigation = navigation;
		_windowService = windowService;
		MyEvents();
	}

	[RelayCommand]
	public void MyEvents()
	{
		Navigation.NavigateTo<MyEventsViewModel>();
	}

	[RelayCommand]
	public void Settings()
	{
		Navigation.NavigateTo<SettingsViewModel>();
	}

	[RelayCommand]
	public void SignOut()
	{
		_windowService.CreateLoginWindow();
		OrganizerIdentityService.Logout();
	}
}
