using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.Admin;

namespace WPF.Views.Admin;

public partial class EventsView : UserControl
{
	public EventsView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<EventsViewModel>();
	}
}
