using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
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
	public async Task BecomeOrganizer()
	{
		
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
