using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace WPF.Viewmodels.Admin;

public partial class SettingsPageViewModel : ObservableObject
{
	[ObservableProperty]
	private MainViewModel _mainVM;

	public SettingsPageViewModel(MainViewModel mainVM)
	{
		MainVM = mainVM;
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
}
