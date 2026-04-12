using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Admin;

public partial class SettingsViewModel : ObservableObject
{
	private IWindowService _windowService { get; init; }

	public SettingsViewModel(
		IWindowService windowService)
	{
		_windowService = windowService;
	}

	[RelayCommand]
	public void SignOut()
	{
		_windowService.CloseMainWindow("Admin MainView");
		// these two will be called MainViewModel.SignOut() when the main window is closed
		// _windowService.CreateLoginWindow();
		// _userIdentityService.Logout();
	}
}
