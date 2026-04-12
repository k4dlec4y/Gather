using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.User;

public partial class MainViewModel : ObservableObject
{
	public IUserIdentityService UserIdentityService { get; init; }
	public INavigationService Navigation { get; init; }
	private IWindowService _windowService { get; init; }

	public MainViewModel(
		IUserIdentityService userIdentityService,
		INavigationService navigation,
		IWindowService windowService)
	{
		Debug.Assert(
			userIdentityService.CurrentUser != null,
			"User cannot be null when initializing MainViewModel");
		UserIdentityService = userIdentityService;

		Navigation = navigation;
		_windowService = windowService;
		Events();
	}

	[RelayCommand]
	public void Events()
	{
		Navigation.NavigateTo<EventsViewModel>();
	}

	[RelayCommand]
	public void Friends()
	{
		Navigation.NavigateTo<FriendsViewModel>();
	}

	[RelayCommand]
	public void Inbox()
	{
		Navigation.NavigateTo<InboxViewModel>();
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
		UserIdentityService.Logout();
	}
}
