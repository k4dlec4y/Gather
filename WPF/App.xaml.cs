using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Services.Abstractions;
using WPF.Services.Implementations;

namespace WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public IServiceProvider Services { get; }
	public new static App Current => (App)Application.Current;

	public App()
	{
		var services = new ServiceCollection();

		services.AddSingleton<IDialogService, WpfDialogService>();

		services.AddTransient<Viewmodels.Admin.EventsPageViewModel>();

		Services = services.BuildServiceProvider();
	}
}
