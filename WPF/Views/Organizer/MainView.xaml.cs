using System.Windows;
using WPF.Models;
using Microsoft.Extensions.DependencyInjection;
using WPF.Viewmodels.Organizer;
using WPF.Services.Abstractions;

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
