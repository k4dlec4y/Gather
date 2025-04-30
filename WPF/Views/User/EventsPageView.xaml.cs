using System.Windows;
using System.Windows.Controls;
using WPF.Models;
using WPF.Viewmodels.UserVM;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views.UserV
{
    /// <summary>
    /// Interaction logic for EventsPageView.xaml
    /// </summary>
    public partial class EventsPageView : Page
    {
		public EventsPageView(MainViewModel main)
		{
			InitializeComponent();
			DataContext = new EventsPageViewModel(main);
		}
	}
}
