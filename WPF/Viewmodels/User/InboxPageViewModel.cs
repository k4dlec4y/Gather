using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Models;
using WPF.Managers;
using System.Diagnostics;

namespace WPF.Viewmodels.UserVM;

public partial class InboxPageViewModel : ObservableObject
{
	[ObservableProperty]
	private MainViewModel _mainVM;

	public InboxPageViewModel(MainViewModel main)
	{
		MainVM = main;
	}

	[RelayCommand]
	private async Task AcceptFriendRequest(FriendRequest friendRequest)
	{
		await FriendRequestManager.AcceptFriendRequest(friendRequest);
	}

	[RelayCommand]
	public async Task RejectFriendRequest(FriendRequest friendRequest)
	{

	}
}
