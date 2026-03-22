using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Managers;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.UserVM;

internal partial class SettingsPageViewModel : ObservableObject
{
    private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }

	[ObservableProperty]
    private MainViewModel _mainVM;

    public SettingsPageViewModel(
		MainViewModel main,
		IDialogService dialogService,
		IWindowService windowService
	) {
        MainVM = main;
        _dialogService = dialogService;
		_windowService = windowService;
	}

    [RelayCommand]
    public void SignOut()
    {
		_windowService.CloseAllWindows();
	}

	[RelayCommand]
	public void BecomeOrganizer()
	{
		_windowService.SendBecomeOrganizerRequest(MainVM.CurrentUser);
	}

	[RelayCommand]
	public async Task DeleteAccount()
	{
		bool success = await UserManager.RemoveUser(MainVM.CurrentUser);
		if (!success)
		{
			_dialogService.ShowError("Please, try again");
			return;
		}
		SignOut();
	}
}
