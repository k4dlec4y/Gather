using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.User;

internal partial class SendInviteViewModel : ObservableObject
{
	private IUserIdentityService _userIdentityService { get; init; }
	private IDialogService _dialogService { get; init; }

	Event _event;

	[ObservableProperty]
	ObservableCollection<Models.User> _friends;

	[ObservableProperty]
	Models.User? _selectedFriend = null;

	public SendInviteViewModel(
		IUserIdentityService userIdentityService,
		IDialogService dialogService,
		Event @event
	) {
		Debug.Assert(userIdentityService.CurrentUser != null, "User should not be null!");
		_userIdentityService = userIdentityService;
		Friends = _userIdentityService.CurrentUser!.Friends;
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
			_userIdentityService.CurrentUser!,
			SelectedFriend,
			_event,
			$"{_userIdentityService.CurrentUser!.Username} has invited you to the event {_event.Name}!"
		);

		// SelectedFriend = null;

		if (success.Equals(""))
			_dialogService.ShowMessage("Invite sent successfully!");
		else
			_dialogService.ShowError(success);
	}
}
