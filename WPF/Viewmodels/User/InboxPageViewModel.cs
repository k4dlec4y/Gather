using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Models;
using WPF.Managers;
using System.Diagnostics;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.User;

internal partial class InboxPageViewModel : ObservableObject
{
	[ObservableProperty]
	public Models.User _currentUser;

	public InboxPageViewModel(
		IUserIdentityService userIdentityService
	) {
		Debug.Assert(userIdentityService.CurrentUser != null, "CurrentUser should not be null when initializing InboxPageViewModel.");
		_currentUser = userIdentityService.CurrentUser;
	}

	[RelayCommand]
	public async Task DeleteMessage(Message message)
	{
		await MessageManager.DeleteMessage(message);
	}

	[RelayCommand]
	private async Task AcceptInvite(Invite invite)
	{
		string response = await InviteManager.AcceptInvite(invite);
		if (response.Equals(""))
		{
			invite.To.Invites.Remove(invite);
		}
		else
		{
			Debug.WriteLine(response);
		}
	}

	[RelayCommand]
	public async Task RejectInvite(Invite invite)
	{
		string response = await InviteManager.DeleteInvite(invite);
		if (response.Equals(""))
		{
			invite.To.Invites.Remove(invite);
		}
		else
		{
			Debug.WriteLine(response);
		}
	}

	[RelayCommand]
	private async Task AcceptFriendRequest(FriendRequest friendRequest)
	{
		await FriendRequestManager.AcceptFriendRequest(friendRequest);
	}

	[RelayCommand]
	public async Task RejectFriendRequest(FriendRequest friendRequest)
	{
		await FriendRequestManager.DeleteFriendRequest(friendRequest);
	}
}
