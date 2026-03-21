using System.Windows;
using WPF.Models;
using WPF.Viewmodels.UserVM;

namespace WPF.Views.UserV;

public partial class SendInviteView : Window
{
    public SendInviteView(MainViewModel mainVM, Event @event)
    {
        InitializeComponent();
        DataContext = new SendInviteViewModel(mainVM, @event, new Services.Implementations.WpfDialogService());
	}
}
