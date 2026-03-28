using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;
using WPF.Viewmodels.Admin;

namespace WPF.Views.Admin;

public partial class MainView : Window
{
    public MainView(User admin)
    {
        InitializeComponent();
        App.Current.Services.GetRequiredService<IUserIdentityService>().Login(admin);
		DataContext = App.Current.Services.GetRequiredService<MainViewModel>();
	}
}
