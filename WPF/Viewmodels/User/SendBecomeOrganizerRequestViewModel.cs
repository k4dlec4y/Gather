using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF.Models;
using WPF.Managers;
using WPF.Services.Abstractions;
using System.Diagnostics;

namespace WPF.Viewmodels.User;

internal partial class SendBecomeOrganizerRequestViewModel : ObservableObject
{
	private IUserIdentityService _userIdentityService { get; init; }
	private IDialogService _dialogService { get; init; }

	[ObservableProperty]
	private string _requestText = "";

	public SendBecomeOrganizerRequestViewModel(
		IUserIdentityService userIdentityService,
		IDialogService dialogService
	) {
		Debug.Assert(userIdentityService.CurrentUser != null, "User cannot be null");
		_userIdentityService = userIdentityService;
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

		string username = _userIdentityService.CurrentUser!.Username;
		if (await BecomeOrganizerRequestManager.ContainsRequest(username))
		{
			_dialogService.ShowError("You already have a pending request!");
			return;
		}

		if (await EventOrganizerManager.GetEventOrganizerByUsername(username) != null)
		{
			_dialogService.ShowError("You are already an organizer!");
			return;
		}

		bool success = await BecomeOrganizerRequestManager.AddRequest(
			new BecomeOrganizerRequest
			{
				UserId = _userIdentityService.CurrentUser!.Id,
				RequestText = RequestText
			}
		);

		if (success)
		{
			_dialogService.ShowMessage("Request sent successfully!");
		}
		else
		{
			_dialogService.ShowError("Failed to send request! Please try again");
		}
	}
}
