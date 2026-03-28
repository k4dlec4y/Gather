using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace WPF.Views.UserV;

public partial class SendBecomeOrganizerRequestView : Window
{
	public SendBecomeOrganizerRequestView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<Viewmodels.User.SendBecomeOrganizerRequestViewModel>();
	}
}
