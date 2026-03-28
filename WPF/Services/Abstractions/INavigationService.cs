using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF.Services.Abstractions;

internal interface INavigationService
{
	void NavigateTo<T>() where T : ObservableObject;
}
