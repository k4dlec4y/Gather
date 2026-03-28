using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class SettingsPageViewModel : ObservableObject
{
	private IOrganizerIdentityService _organizerIdentityService { get; init; }
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }

	public SettingsPageViewModel(
		IOrganizerIdentityService organizerIdentityService,
		IDialogService dialogService,
		IWindowService windowService)
	{
		_organizerIdentityService = organizerIdentityService;
		_dialogService = dialogService;
		_windowService = windowService;
	}

	[RelayCommand]
	public void SignOut()
	{
		_windowService.CreateLoginWindow();
		_windowService.CloseMainWindow("Organizer MainView");
		_organizerIdentityService.Logout();
	}

	[RelayCommand]
	public async Task DeleteAccount()
	{
		bool success = await Managers.EventOrganizerManager.DeleteEventOrganizer(
			_organizerIdentityService.CurrentEventOrganizer);
		if (!success)
		{
			_dialogService.ShowError("Please, try again");
			return;
		}
		SignOut();
	}
}
