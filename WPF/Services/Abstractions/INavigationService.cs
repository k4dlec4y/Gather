using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF.Services.Abstractions;

public interface INavigationService
{
	void NavigateTo<T>() where T : ObservableObject;
}
