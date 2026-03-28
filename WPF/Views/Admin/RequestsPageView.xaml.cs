using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.Admin;

namespace WPF.Views.Admin;

public partial class RequestsPageView : UserControl
{
	public RequestsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<RequestsPageViewModel>();
	}
}
