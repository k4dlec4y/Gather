using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using WPF.Views.Admin;

namespace WPF.Viewmodels.Admin;

public partial class MainViewModel : ObservableObject
{
	[ObservableProperty]
	private Page _currentPage;

	public MainViewModel()
	{
		CurrentPage = new EventsPageView();
	}

	[RelayCommand]
	public void Events()
	{
		CurrentPage = new EventsPageView();
	}

	[RelayCommand]
	public void Requests()
	{
		CurrentPage = new RequestsPageView();
	}

	[RelayCommand]
	public void Settings()
	{
		CurrentPage = new SettingsPageView(this);
	}
}

