using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Admin;

internal partial class MainViewModel : ObservableObject
{
	public INavigationService Navigation { get; init; }
	private IWindowService _windowService { get; init; }
	private IUserIdentityService _userIdentityService { get; init; }

	public MainViewModel(
		IWindowService windowService,
		IUserIdentityService userIdentityService,
		INavigationService navigation)
	{
		_windowService = windowService;
		_userIdentityService = userIdentityService;
		Navigation = navigation;
		Events();
	}

	[RelayCommand]
	public void Events()
	{
		Navigation.NavigateTo<EventsPageViewModel>();
	}

	[RelayCommand]
	public void Requests()
	{
		Navigation.NavigateTo<RequestsPageViewModel>();
	}

	[RelayCommand]
	public void Settings()
	{
		Navigation.NavigateTo<SettingsPageViewModel>();
	}

	[RelayCommand]
	public void SignOut()
	{
		_windowService.CreateLoginWindow();
		_userIdentityService.Logout();
	}
}
