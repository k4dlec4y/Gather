using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Models;

namespace WPF.Viewmodels.Admin;

public partial class RequestsPageViewModel : ObservableObject
{
	public ObservableCollection<BecomeOrganizerRequest> BecomeOrganizerRequests { get; set; } =
		Managers.BecomeOrganizerRequestManager.GetRequests();

	[RelayCommand]
	private async Task AcceptRequest(BecomeOrganizerRequest bor)
	{
		if (bor != null)
		{
			await Managers.BecomeOrganizerRequestManager.AcceptRequest(bor);
			BecomeOrganizerRequests.Remove(bor);
			OnPropertyChanged(nameof(BecomeOrganizerRequests));
		}
	}

	[RelayCommand]
	private async Task RejectRequest(BecomeOrganizerRequest bor)
	{
		if (bor != null)
		{
			await Managers.BecomeOrganizerRequestManager.RemoveRequest(bor);
			BecomeOrganizerRequests.Remove(bor);
			OnPropertyChanged(nameof(BecomeOrganizerRequests));
		}
	}

	[RelayCommand]
	private void ReloadRequests()
	{
		BecomeOrganizerRequests = Managers.BecomeOrganizerRequestManager.GetRequests();
		OnPropertyChanged(nameof(BecomeOrganizerRequests));
	}
}
