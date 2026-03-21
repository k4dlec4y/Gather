using System.Windows.Controls;

namespace WPF.Views.Admin;

public partial class RequestsPageView : Page
{
    public RequestsPageView()
    {
        InitializeComponent();
		DataContext = new Viewmodels.Admin.RequestsPageViewModel();
	}
}
