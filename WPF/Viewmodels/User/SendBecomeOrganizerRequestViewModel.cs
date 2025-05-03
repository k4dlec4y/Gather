using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace WPF.Viewmodels.UserVM
{
    public partial class SendBecomeOrganizerRequestViewModel : ObservableObject
    {
		[ObservableProperty]
		private string _requestText = "";

		private Models.User _currentUser;
		private Window _sendRequestWindow;

		public SendBecomeOrganizerRequestViewModel(Window sendRequestWindow, Models.User user)
		{
			_currentUser = user;
			_sendRequestWindow = sendRequestWindow;
		}

		[RelayCommand]
		public async Task SendRequest()
		{
			if (string.IsNullOrWhiteSpace(RequestText))
			{
				MessageBox.Show("Request text cannot be empty!");
				return;
			}

			if (await Managers.BecomeOrganizerRequestManager.ContainsRequest(_currentUser.Username))
			{
				MessageBox.Show("You already have a pending request!");
				return;
			}

			bool success = await Managers.BecomeOrganizerRequestManager.AddRequest(
				new Models.BecomeOrganizerRequest
				{
					UserId = _currentUser.Id,
					RequestText = RequestText
				}
			);

			if (success)
			{
				MessageBox.Show("Request sent successfully!");
				_sendRequestWindow.Close();
			}
			else
			{
				MessageBox.Show("Failed to send request!");
			}
		}
	}
}
