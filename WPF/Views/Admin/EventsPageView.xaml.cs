using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.Admin;

namespace WPF.Views.Admin;

public partial class EventsPageView : Page
{
    public EventsPageView(MainViewModel mainVM)
    {
        InitializeComponent();
		DataContext = App.Current.Services.GetService<EventsPageViewModel>();
	}
}
