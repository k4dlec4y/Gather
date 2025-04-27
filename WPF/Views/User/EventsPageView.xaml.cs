using System.Windows;
using System.Windows.Controls;
using WPF.Models;

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
			EventList.ItemsSource = Managers.EventManager.GetEvents();
		}

		private void EventList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (EventList.SelectedItem is Event selectedEvent)
			{
				MessageBox.Show($"Clicked on: {selectedEvent.Name}");
			}

			// Optional: reset selection so click works multiple times
			EventList.SelectedItem = null;
		}
	}
}
