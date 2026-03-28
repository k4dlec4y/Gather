using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.Admin;

namespace WPF.Views.Admin;

public partial class SettingsPageView : UserControl
{
	public SettingsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<SettingsPageViewModel>();
	}
}
