using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Models;
using WPF.Viewmodels.User;

namespace WPF.Views.UserV;

public partial class SendInviteView : Window
{
	public SendInviteView(Event @event)
	{
		InitializeComponent();
		DataContext = ActivatorUtilities.CreateInstance<SendInviteViewModel>(App.Current.Services, @event);
	}
}
