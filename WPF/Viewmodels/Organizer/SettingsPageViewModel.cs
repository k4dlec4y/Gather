using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class SettingsPageViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }

	[ObservableProperty]
	private MainViewModel _mainVM;

	public SettingsPageViewModel(
		MainViewModel mainVM,
		IDialogService dialogService,
		IWindowService windowService
	) {
		MainVM = mainVM;
		_dialogService = dialogService;
		_windowService = windowService;
	}

	[RelayCommand]
	public void SignOut()
	{
		_windowService.CloseAllWindows();
	}

	[RelayCommand]
	public async Task DeleteAccount()
	{
		bool success = await Managers.EventOrganizerManager.DeleteEventOrganizer(MainVM.EventOrganizer);
		if (!success)
		{
			_dialogService.ShowError("Please, try again");
			return;
		}
		SignOut();
	}
}
