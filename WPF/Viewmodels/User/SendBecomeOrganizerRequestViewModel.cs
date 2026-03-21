using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.UserVM;

internal partial class SendBecomeOrganizerRequestViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }

	[ObservableProperty]
	private string _requestText = "";

	private Models.User _currentUser;
	private Window _sendRequestWindow;

	public SendBecomeOrganizerRequestViewModel
		(Window sendRequestWindow, Models.User user, IDialogService dialogService)
	{
		_currentUser = user;
		_sendRequestWindow = sendRequestWindow;
		_dialogService = dialogService;
	}

	[RelayCommand]
	public async Task SendRequest()
	{
		if (string.IsNullOrWhiteSpace(RequestText))
		{
			_dialogService.ShowError("Request text cannot be empty!");
			return;
		}

		if (RequestText.Length > 200)
		{
			_dialogService.ShowError($"Your description exceeded the maximum size by {RequestText.Length - 200} characters!");
			return;
		}

		if (await Managers.BecomeOrganizerRequestManager.ContainsRequest(_currentUser.Username))
		{
			_dialogService.ShowError("You already have a pending request!");
			return;
		}

		if (await Managers.EventOrganizerManager.GetEventOrganizerByUsername(_currentUser.Username) != null)
		{
			_dialogService.ShowError("You are already an organizer!");
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
			_dialogService.ShowMessage("Request sent successfully!");
			_sendRequestWindow.Close();
		}
		else
		{
			_dialogService.ShowError("Failed to send request! Please try again");
		}
	}
}
