using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Services.Abstractions;
using WPF.Services.Implementations;

namespace WPF;

public partial class App : Application
{
	public IServiceProvider Services { get; }
	public new static App Current => (App)Application.Current;

	public App()
	{
		var services = new ServiceCollection();

		services.AddSingleton<IDialogService, WpfDialogService>();
		services.AddSingleton<IWindowService, WpfWindowService>();
		services.AddSingleton<INavigationService, WpfNavigationService>();
		services.AddSingleton<IFileService, DefaultFileService>();
		services.AddSingleton<IUserIdentityService, UserIdentityService>();
		services.AddSingleton<IOrganizerIdentityService, OrganizerIdentityService>();

		services.AddTransient<Viewmodels.Admin.MainViewModel>();
		services.AddTransient<Viewmodels.Admin.EventsViewModel>();
		services.AddTransient<Viewmodels.Admin.RequestsViewModel>();
		services.AddTransient<Viewmodels.Admin.SettingsViewModel>();

		services.AddTransient<Viewmodels.Organizer.MainViewModel>();
		services.AddTransient<Viewmodels.Organizer.MyEventsViewModel>();
		services.AddTransient<Viewmodels.Organizer.CreateEventViewModel>();
		services.AddTransient<Viewmodels.Organizer.EditEventViewModel>();
		services.AddTransient<Viewmodels.Organizer.SettingsViewModel>();

		services.AddTransient<Viewmodels.User.MainViewModel>();
		services.AddTransient<Viewmodels.User.EventsViewModel>();
		services.AddTransient<Viewmodels.User.FriendsViewModel>();
		services.AddTransient<Viewmodels.User.InboxViewModel>();
		services.AddTransient<Viewmodels.User.SendBecomeOrganizerRequestViewModel>();
		services.AddTransient<Viewmodels.User.SendInviteViewModel>();
		services.AddTransient<Viewmodels.User.SettingsViewModel>();

		services.AddSingleton<Viewmodels.LoginViewModel>();
		services.AddTransient<Viewmodels.EventDetailsViewModel>();

		Services = services.BuildServiceProvider();
	}
}
