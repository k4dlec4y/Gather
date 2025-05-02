using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF.Managers;
using WPF.Models;

namespace WPF.Viewmodels.UserVM
{
	public partial class FriendsPageViewModel : ObservableObject
	{
		[ObservableProperty]
		private string _newFriendUsername;

		[ObservableProperty]
		private string _selectedUser;

		[ObservableProperty]
		private MainViewModel _mainVM;

		public FriendsPageViewModel(MainViewModel main)
		{
			MainVM = main;
		}

		[RelayCommand]
		public async Task AddFriend()
		{
			(bool userExists, User? to) = await UserManager.GetUser(NewFriendUsername);

			if(NewFriendUsername == MainVM.CurrentUser.Username)
			{
				MessageBox.Show("You cannot add yourself as a friend");
				return;
			}
			if (!userExists || to == null)
			{
				MessageBox.Show("This user doesnt exist");
				return;
			}
			if (MainVM.CurrentUser.Friends.Select(f => f.Username).Contains(to.Username))
			{
				MessageBox.Show($"You are already in friendship with {to.Username}");
				return;
			}
			if (await FriendRequestManager.ContainsFriendRequest(MainVM.CurrentUser, to))
			{
				MessageBox.Show($"You have already sent a friend request to {to.Username}");
				return;
			}

			bool sent = await FriendRequestManager.SendFriendRequest(
				MainVM.CurrentUser,
				to,
				$"{MainVM.CurrentUser.Username} has sent you a friend request!");

			MessageBox.Show(sent ? $"Friend request sent" : "Please, try again");

			
		}

		[RelayCommand]
		public async Task RemoveFriend()
		{
			//await FriendRequestManager.DeleteFriendship(MainVM.User.Friends.FirstOrDefault(f => f.Username == SelectedUser));
		}
	}
}
