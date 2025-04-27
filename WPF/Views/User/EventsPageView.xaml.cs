using System.Windows;
using System.Windows.Controls;
using WPF.Models;
using WPF.Viewmodels.User;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views.User
{
    /// <summary>
    /// Interaction logic for EventsPageView.xaml
    /// </summary>
    public partial class EventsPageView : Page
    {
		public EventsPageView()
		{
			InitializeComponent();
			DataContext = new EventsPageViewModel();
		}
	}
}
