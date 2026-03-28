using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace WPF.Views.Organizer;

public partial class SettingsView : UserControl
{
	public SettingsView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<Viewmodels.Organizer.SettingsViewModel>();
	}
}
