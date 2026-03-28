using System.Windows.Controls;
using WPF.Viewmodels.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views.Admin;

public partial class SettingsPageView : UserControl
{
	public SettingsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<SettingsPageViewModel>();
	}
}
