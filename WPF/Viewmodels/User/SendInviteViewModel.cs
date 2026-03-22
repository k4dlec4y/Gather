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

	User _user;
	Event _event;

	[ObservableProperty]
	ObservableCollection<User> _friends;

	[ObservableProperty]
	User? _selectedFriend = null;

	public SendInviteViewModel(User user, Event @event, IDialogService dialogService)
	{
		_user = user;
		Friends = _user.Friends;
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

		string success = await Managers.InviteManager.SendInvite(
			_user,
			SelectedFriend,
			_event,
			$"{_user.Username} has invited you to the event {_event.Name}!"
		);

		// SelectedFriend = null;

		if (success.Equals(""))
			_dialogService.ShowMessage("Invite sent successfully!");
		else
			_dialogService.ShowError(success);
	}
}
