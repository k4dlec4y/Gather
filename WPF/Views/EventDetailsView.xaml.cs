using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;

namespace WPF.Views;

public partial class EventDetailsView : Window
{
	public EventDetailsView(Event selectedEvent, ObservableCollection<User> friends)
	{
		InitializeComponent();
		DataContext = ActivatorUtilities.CreateInstance<Viewmodels.EventDetailsViewModel>(App.Current.Services, selectedEvent, friends);
	}
}
