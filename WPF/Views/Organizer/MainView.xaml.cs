using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;
using WPF.Viewmodels.Organizer;

namespace WPF.Views.Organizer;

public partial class MainView : Window
{
	public MainView(EventOrganizer organizer)
	{
		InitializeComponent();
		App.Current.Services.GetRequiredService<IOrganizerIdentityService>().Login(organizer);
		DataContext = App.Current.Services.GetRequiredService<MainViewModel>();
	}
}
