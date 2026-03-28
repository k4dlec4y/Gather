using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.Admin;

namespace WPF.Views.Admin;

public partial class SettingsView : UserControl
{
	public SettingsView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<SettingsViewModel>();
	}
}
