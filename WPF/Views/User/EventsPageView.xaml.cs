using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.User;

namespace WPF.Views.UserV;

public partial class EventsPageView : UserControl
{
	public EventsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<EventsPageViewModel>();
	}
}
