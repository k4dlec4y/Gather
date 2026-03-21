using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.UserVM;

internal partial class SettingsPageViewModel : ObservableObject
{
    private IDialogService _dialogService { get; init; }

    [ObservableProperty]
    private MainViewModel _mainVM;

    public SettingsPageViewModel(MainViewModel main, IDialogService dialogService)
    {
        MainVM = main;
        _dialogService = dialogService;	
    }

    [RelayCommand]
    public void SignOut()
    {
		foreach (Window window in Application.Current.Windows)
		{
			if (window.DataContext == MainVM)
			{
				window.Close();
				break;
			}
		}
	}

	[RelayCommand]
	public void BecomeOrganizer()
	{
		Window request = new Views.UserV.SendBecomeOrganizerRequestView(MainVM.CurrentUser);
		request.Show();
	}

	[RelayCommand]
	public async Task DeleteAccount()
	{
		bool success = await Managers.UserManager.RemoveUser(MainVM.CurrentUser);
		if (!success)
		{
			_dialogService.ShowError("Please, try again");
			return;
		}
		foreach (Window window in Application.Current.Windows)
		{
			if (window.DataContext == MainVM)
			{
				window.Close();
				break;
			}
		}
	}
}
