using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace WPF.Views.UserV;

public partial class SettingsPageView : UserControl
{
	public SettingsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<Viewmodels.User.SettingsPageViewModel>();
	}
}
