using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using WPF.Services.Abstractions;

namespace WPF.Services.Implementations;

internal partial class WpfNavigationService : ObservableObject, INavigationService
{
	private readonly IServiceProvider _serviceProvider;

	[ObservableProperty]
	private ObservableObject? _currentPage;

	public WpfNavigationService(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public void NavigateTo<T>() where T : ObservableObject
	{
		var viewModel = _serviceProvider.GetRequiredService<T>();
		CurrentPage = viewModel;
	}
}
