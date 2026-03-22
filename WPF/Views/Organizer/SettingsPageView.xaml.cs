using System.Windows.Controls;

namespace WPF.Views.Organizer;

public partial class SettingsPageView : Page
{
    public SettingsPageView(Viewmodels.Organizer.MainViewModel mainVM)
    {
        InitializeComponent();
		DataContext = new Viewmodels.Organizer.SettingsPageViewModel(
            mainVM,
            new Services.Implementations.WpfDialogService(),
			new Services.Implementations.WpfWindowService()
		);
	}
}
