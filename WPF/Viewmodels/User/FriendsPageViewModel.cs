using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF.Managers;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.UserVM;

internal partial class FriendsPageViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }

	[ObservableProperty]
	private string _newFriendUsername = "";

	[ObservableProperty]
	private string _selectedUser = "";

	[ObservableProperty]
	private MainViewModel _mainVM;

	public FriendsPageViewModel(MainViewModel main, IDialogService dialogService)
	{
		MainVM = main;
		_dialogService = dialogService;
	}

	[RelayCommand]
	public async Task AddFriend()
	{
		User? to = await UserManager.GetUser(NewFriendUsername);

		if(NewFriendUsername == MainVM.CurrentUser.Username)
		{
			_dialogService.ShowError("You cannot add yourself as a friend");
			return;
		}
		if (to == null)
		{
			_dialogService.ShowError("This user doesnt exist");
			return;
		}
		if (MainVM.CurrentUser.Friends.Select(f => f.Username).Contains(to.Username))
		{
			_dialogService.ShowError($"You are already in friendship with {to.Username}");
			return;
		}
		if (await FriendRequestManager.ContainsFriendRequest(MainVM.CurrentUser, to))
		{
			_dialogService.ShowError($"You have already sent a friend request to {to.Username}");
			return;
		}

		bool sent = await FriendRequestManager.SendFriendRequest(
			MainVM.CurrentUser,
			to,
			$"{MainVM.CurrentUser.Username} has sent you a friend request!");

		_dialogService.ShowMessage(sent ? $"Friend request sent" : "Please, try again");	
	}

	[RelayCommand]
	public async Task RemoveFriend(User friend)
	{
		if (await FriendshipManager.DeleteFriendship(MainVM.CurrentUser, friend))
		{
			MainVM.CurrentUser.Friends.Remove(friend);
			OnPropertyChanged(nameof(MainVM.CurrentUser.Friends));
		}
	}
}
