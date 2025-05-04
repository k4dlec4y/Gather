using System.Windows.Controls;

namespace WPF.Views.Admin;

public partial class EventsPageView : Page
{
    public EventsPageView()
    {
        InitializeComponent();
		DataContext = new Viewmodels.Admin.EventsPageViewModel();
	}
}
