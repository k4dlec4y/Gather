using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WPF.Managers;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.User;

internal partial class SettingsViewModel : ObservableObject
{
	private IUserIdentityService _userIdentityService { get; init; }
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }

	public SettingsViewModel(
		IUserIdentityService userIdentityService,
		IDialogService dialogService,
		IWindowService windowService)
	{
		Debug.Assert(userIdentityService.CurrentUser != null, "User should not be null!");
		_userIdentityService = userIdentityService;
		_dialogService = dialogService;
		_windowService = windowService;
	}

	[RelayCommand]
	public void SignOut()
	{
		_windowService.CloseMainWindow("User MainView");
		// these two will be called MainViewModel.SignOut() when the main window is closed
		// _windowService.CreateLoginWindow();
		// _userIdentityService.Logout();
	}

	[RelayCommand]
	public void BecomeOrganizer()
	{
		_windowService.SendBecomeOrganizerRequest();
	}

	[RelayCommand]
	public async Task DeleteAccount()
	{
		Debug.Assert(
			_userIdentityService.CurrentUser != null,
			"User should not be null when deleting account!");
		bool success = await UserManager.RemoveUser(_userIdentityService.CurrentUser);
		if (!success)
		{
			_dialogService.ShowError("Please, try again");
			return;
		}
		SignOut();
	}
}
