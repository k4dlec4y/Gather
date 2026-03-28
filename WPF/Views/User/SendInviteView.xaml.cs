using System.Windows;
using WPF.Models;
using WPF.Viewmodels.User;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views.UserV;

public partial class SendInviteView : Window
{
    public SendInviteView(Event @event)
    {
        InitializeComponent();
        DataContext = ActivatorUtilities.CreateInstance<SendInviteViewModel>(App.Current.Services, @event);
	}
}
