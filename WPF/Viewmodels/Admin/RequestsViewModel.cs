using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Managers;
using WPF.Models;

namespace WPF.Viewmodels.Admin;

public partial class RequestsViewModel : ObservableObject
{
	public ObservableCollection<BecomeOrganizerRequest> Requests { get; set; } =
		BecomeOrganizerRequestManager.GetRequests();

	[RelayCommand]
	private async Task AcceptRequest(BecomeOrganizerRequest bor)
	{
		if (bor != null)
		{
			await BecomeOrganizerRequestManager.AcceptRequest(bor);
			Requests.Remove(bor);
		}
	}

	[RelayCommand]
	private async Task RejectRequest(BecomeOrganizerRequest bor)
	{
		if (bor != null)
		{
			await BecomeOrganizerRequestManager.RemoveRequest(bor);
			Requests.Remove(bor);
		}
	}

	[RelayCommand]
	private void ReloadRequests()
	{
		Requests = BecomeOrganizerRequestManager.GetRequests();
		OnPropertyChanged(nameof(Requests));
	}
}
