using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Managers;
using WPF.Models;

namespace WPF.Viewmodels.Admin;

public partial class RequestsPageViewModel : ObservableObject
{
	public ObservableCollection<BecomeOrganizerRequest> BecomeOrganizerRequests { get; set; } =
		BecomeOrganizerRequestManager.GetRequests();

	[RelayCommand]
	private async Task AcceptRequest(BecomeOrganizerRequest bor)
	{
		if (bor != null)
		{
			await BecomeOrganizerRequestManager.AcceptRequest(bor);
			BecomeOrganizerRequests.Remove(bor);
		}
	}

	[RelayCommand]
	private async Task RejectRequest(BecomeOrganizerRequest bor)
	{
		if (bor != null)
		{
			await BecomeOrganizerRequestManager.RemoveRequest(bor);
			BecomeOrganizerRequests.Remove(bor);
		}
	}

	[RelayCommand]
	private void ReloadRequests()
	{
		BecomeOrganizerRequests = BecomeOrganizerRequestManager.GetRequests();
		OnPropertyChanged(nameof(BecomeOrganizerRequests));
	}
}
