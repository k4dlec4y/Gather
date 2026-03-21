using System.Windows.Controls;

namespace WPF.Views.Organizer;

public partial class MyEventsPageView : Page
{
    public MyEventsPageView(Viewmodels.Organizer.MainViewModel mainVM)
    {
        InitializeComponent();
        DataContext = new Viewmodels.Organizer.MyEventsPageViewModel(mainVM, new Services.Implementations.WpfDialogService());
	}
}
