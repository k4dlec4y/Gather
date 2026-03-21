using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace WPF.Viewmodels.UserVM;

public partial class SettingsPageViewModel : ObservableObject
{
    [ObservableProperty]
    private MainViewModel _mainVM;

    public SettingsPageViewModel(MainViewModel main)
    {
        MainVM = main;
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
			MessageBox.Show("Please, try again");
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
