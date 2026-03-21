using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class SettingsPageViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }

	[ObservableProperty]
	private MainViewModel _mainVM;

	public SettingsPageViewModel(MainViewModel mainVM, IDialogService dialogService)
	{
		MainVM = mainVM;
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
