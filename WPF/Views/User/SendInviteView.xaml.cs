using System.Windows;
using WPF.Models;
using WPF.Viewmodels.UserVM;

namespace WPF.Views.UserV;

public partial class SendInviteView : Window
{
    public SendInviteView(User user, Event @event)
    {
        InitializeComponent();
        DataContext = new SendInviteViewModel(user, @event, new Services.Implementations.WpfDialogService());
	}
}
