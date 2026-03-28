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
		services.AddTransient<Viewmodels.Admin.EventsPageViewModel>();
		services.AddTransient<Viewmodels.Admin.RequestsPageViewModel>();
		services.AddTransient<Viewmodels.Admin.SettingsPageViewModel>();

		services.AddTransient<Viewmodels.Organizer.MainViewModel>();
		services.AddTransient<Viewmodels.Organizer.MyEventsPageViewModel>();
		services.AddTransient<Viewmodels.Organizer.CreateEventWindowViewModel>();
		services.AddTransient<Viewmodels.Organizer.EditEventWindowViewModel>();
		services.AddTransient<Viewmodels.Organizer.SettingsPageViewModel>();

		services.AddTransient<Viewmodels.User.MainViewModel>();
		services.AddTransient<Viewmodels.User.EventsPageViewModel>();
		services.AddTransient<Viewmodels.User.FriendsPageViewModel>();
		services.AddTransient<Viewmodels.User.InboxPageViewModel>();
		services.AddTransient<Viewmodels.User.SendBecomeOrganizerRequestViewModel>();
		services.AddTransient<Viewmodels.User.SendInviteViewModel>();
		services.AddTransient<Viewmodels.User.SettingsPageViewModel>();

		services.AddSingleton<Viewmodels.LoginViewModel>();
		services.AddTransient<Viewmodels.EventDetailsViewModel>();

		Services = services.BuildServiceProvider();
	}
}
