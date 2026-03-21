using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.UserVM;

internal partial class SendInviteViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }

	MainViewModel _mainVM;
	Event _event;

	[ObservableProperty]
	ObservableCollection<User> _friends;

	[ObservableProperty]
	User? _selectedFriend = null;

	public SendInviteViewModel(MainViewModel mainVM, Event @event, IDialogService dialogService)
	{
		_mainVM = mainVM;
		Friends = mainVM.CurrentUser.Friends;
		_event = @event;
		_dialogService = dialogService;
	}

	[RelayCommand]
	public async Task Send()
	{
		if (SelectedFriend == null)
		{
			_dialogService.ShowError("No friend selected");
			return;
		}

		string response = await Managers.InviteManager.SendInvite(
			_mainVM.CurrentUser,
			SelectedFriend,
			_event,
			$"{_mainVM.CurrentUser.Username} has invited you to the event {_event.Name}!"
		);

		//SelectedFriend = null;

		_dialogService.ShowMessage(response.Equals("") ? "Invite sent successfully!" : response);
	}
}
