using System.Windows;
using WPF.Models;

namespace WPF.Views.UserV;

public partial class SendBecomeOrganizerRequestView : Window
{
    public SendBecomeOrganizerRequestView(User user)
    {
		InitializeComponent();
		DataContext = new Viewmodels.UserVM.SendBecomeOrganizerRequestViewModel(this, user, new Services.Implementations.WpfDialogService());
	}
}
