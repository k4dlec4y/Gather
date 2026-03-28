using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.User;

internal partial class MainViewModel : ObservableObject
{
	public IUserIdentityService UserIdentityService { get; init; }
	public INavigationService Navigation { get; init; }
	private IWindowService _windowService { get; init; }

	public MainViewModel(
		IUserIdentityService userIdentityService,
		INavigationService navigation,
		IWindowService windowService
	) {
		Debug.Assert(userIdentityService.CurrentUser != null, "User cannot be null");
		UserIdentityService = userIdentityService;
		Navigation = navigation;
		_windowService = windowService;
		Events();
	}

    [RelayCommand]
    public void Events()
    {
		Navigation.NavigateTo<EventsPageViewModel>();
	}

	[RelayCommand]
	public void Friends()
    {
		Navigation.NavigateTo<FriendsPageViewModel>();
	}

	[RelayCommand]
	public void Inbox()
	{
		Navigation.NavigateTo<InboxPageViewModel>();
	}

	[RelayCommand]
	public void Settings()
    {
		Navigation.NavigateTo<SettingsPageViewModel>();
	}

	[RelayCommand]
	public void SignOut()
	{
		UserIdentityService.Logout();
		_windowService.ShowLoginWindow();
	}
}
