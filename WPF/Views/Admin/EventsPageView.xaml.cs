using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.Admin;

namespace WPF.Views.Admin;

public partial class EventsPageView : UserControl
{
	public EventsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<EventsPageViewModel>();
	}
}
