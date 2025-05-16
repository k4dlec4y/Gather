using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using WPF.Models;

namespace WPF.Viewmodels.UserVM;

public partial class SendInviteViewModel : ObservableObject
{
	MainViewModel _mainVM;
	Event _event;

	[ObservableProperty]
	ObservableCollection<User> _friends;

	[ObservableProperty]
	User? _selectedFriend = null;

	public SendInviteViewModel(MainViewModel mainVM, Event @event)
	{
		_mainVM = mainVM;
		Friends = mainVM.CurrentUser.Friends;
		_event = @event;
	}

	[RelayCommand]
	public async Task Send()
	{
		if (SelectedFriend == null)
		{
			MessageBox.Show("No friend selected");
			Debug.WriteLine("No friend selected");
			return;
		}

		string response = await Managers.InviteManager.SendInvite(
			_mainVM.CurrentUser,
			SelectedFriend,
			_event,
			$"{_mainVM.CurrentUser.Username} has invited you to the event {_event.Name}!"
		);

		//SelectedFriend = null;

		MessageBox.Show(response.Equals("") ? "Invite sent successfully!" : response);
	}
}
