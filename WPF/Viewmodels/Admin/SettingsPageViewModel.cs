using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Admin;

internal partial class SettingsPageViewModel : ObservableObject
{
	private IWindowService _windowService { get; init; }

	public SettingsPageViewModel(IWindowService windowService)
	{
		_windowService = windowService;
	}

	[RelayCommand]
	public void SignOut()
	{
		_windowService.CloseAllWindows();
	}
}
