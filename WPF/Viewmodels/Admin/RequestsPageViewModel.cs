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
	private async Task AcceptRequest(BecomeOrganizerRequest bro)
	{
		if (bro != null)
		{
			await Managers.BecomeOrganizerRequestManager.AcceptRequest(bro);
			BecomeOrganizerRequests.Remove(bro);
		}
	}

	[RelayCommand]
	private async Task RejectRequest(BecomeOrganizerRequest bro)
	{
		if (bro != null)
		{
			await Managers.BecomeOrganizerRequestManager.RemoveRequest(bro);
			BecomeOrganizerRequests.Remove(bro);
		}
	}

	[RelayCommand]
	private void ReloadRequests()
	{
		BecomeOrganizerRequests = Managers.BecomeOrganizerRequestManager.GetRequests();
		OnPropertyChanged(nameof(BecomeOrganizerRequests));
	}
}
