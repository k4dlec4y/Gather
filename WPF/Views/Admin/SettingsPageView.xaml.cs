using System.Windows.Controls;

namespace WPF.Views.Admin;

public partial class SettingsPageView : Page
{
	public SettingsPageView(Viewmodels.Admin.MainViewModel mainVM)
	{
		InitializeComponent();
		DataContext = new Viewmodels.Admin.SettingsPageViewModel(new Services.Implementations.WpfWindowService());
	}
}
