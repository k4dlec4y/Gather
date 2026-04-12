using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WPF.Managers;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.User;

public partial class FriendsViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }

	[ObservableProperty]
	private Models.User _currentUser;

	[ObservableProperty]
	private string _newFriendUsername = "";

	[ObservableProperty]
	private string _selectedUser = "";

	public FriendsViewModel(
		IUserIdentityService userIdentityService,
		IDialogService dialogService)
	{
		Debug.Assert(
			userIdentityService.CurrentUser != null,
			"CurrentUser should not be null when initializing FriendsPageViewModel");
		CurrentUser = userIdentityService.CurrentUser;
		_dialogService = dialogService;
	}

	[RelayCommand]
	public async Task AddFriend()
	{
		Models.User? to = await UserManager.GetUser(NewFriendUsername);

		if (NewFriendUsername == CurrentUser.Username)
		{
			_dialogService.ShowError("You cannot add yourself as a friend");
			return;
		}
		if (to == null)
		{
			_dialogService.ShowError("This user doesnt exist");
			return;
		}
		if (CurrentUser.Friends.Select(f => f.Username).Contains(to.Username))
		{
			_dialogService.ShowError($"You are already in friendship with {to.Username}");
			return;
		}
		if (await FriendRequestManager.ContainsFriendRequest(CurrentUser, to))
		{
			_dialogService.ShowError($"You have already sent a friend request to {to.Username}");
			return;
		}

		bool sent = await FriendRequestManager.SendFriendRequest(
			CurrentUser,
			to,
			$"{CurrentUser.Username} has sent you a friend request!");

		if (sent)
		{
			_dialogService.ShowMessage($"Friend request sent");
		}
		else
		{
			_dialogService.ShowError("Please, try again");
		}
	}

	[RelayCommand]
	public async Task RemoveFriend(Models.User friend)
	{
		if (await FriendshipManager.DeleteFriendship(CurrentUser, friend))
		{
			CurrentUser.Friends.Remove(friend);
		}
	}
}
