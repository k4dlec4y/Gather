using System.Windows.Controls;
using WPF.Viewmodels.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views.Admin;

public partial class RequestsPageView : UserControl
{
    public RequestsPageView()
    {
        InitializeComponent();
		DataContext = App.Current.Services.GetService<RequestsPageViewModel>();
	}
}
