using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace WPF.Views.UserV;

public partial class SettingsView : UserControl
{
	public SettingsView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<Viewmodels.User.SettingsViewModel>();
	}
}
