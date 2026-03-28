using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.Admin;

namespace WPF.Views.Admin;

public partial class RequestsView : UserControl
{
	public RequestsView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<RequestsViewModel>();
	}
}
