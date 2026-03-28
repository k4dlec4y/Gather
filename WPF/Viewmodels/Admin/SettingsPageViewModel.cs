using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Admin;

internal partial class SettingsPageViewModel : ObservableObject
{
	private IWindowService _windowService { get; init; }
	private IUserIdentityService _userIdentityService { get; init; }

	public SettingsPageViewModel(
		IWindowService windowService,
		IUserIdentityService userIdentityService
	) {
		_windowService = windowService;
		_userIdentityService = userIdentityService;
	}

	[RelayCommand]
	public void SignOut()
	{
		_windowService.CreateLoginWindow();
		_windowService.CloseMainWindow("Admin MainView");
		_userIdentityService.Logout();
	}
}
