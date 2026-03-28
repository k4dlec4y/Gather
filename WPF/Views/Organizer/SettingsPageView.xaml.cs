using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace WPF.Views.Organizer;

public partial class SettingsPageView : UserControl
{
	public SettingsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<Viewmodels.Organizer.SettingsPageViewModel>();
	}
}
