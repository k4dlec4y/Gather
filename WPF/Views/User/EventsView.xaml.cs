using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.User;

namespace WPF.Views.UserV;

public partial class EventsView : UserControl
{
	public EventsView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<EventsViewModel>();
	}
}
