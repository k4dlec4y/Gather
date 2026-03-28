using System.Windows;
using System.Windows.Controls;
using WPF.Viewmodels;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views;

public partial class LoginView : Window
{
	public LoginView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetRequiredService<LoginViewModel>();
	}

	/* could not figure better way to work this out */
	private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
	{
		if (DataContext != null)
		{ ((LoginViewModel)DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword; }
	}
}
