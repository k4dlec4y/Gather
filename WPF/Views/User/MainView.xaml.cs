using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Services.Abstractions;

namespace WPF.Views.UserV;

public partial class MainView : Window
{
	public MainView(Models.User user)
	{
		InitializeComponent();
		App.Current.Services.GetRequiredService<IUserIdentityService>().Login(user);
		DataContext = App.Current.Services.GetRequiredService<Viewmodels.User.MainViewModel>();
	}
}
