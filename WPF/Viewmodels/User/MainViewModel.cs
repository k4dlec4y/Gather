using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using WPF.Views.UserV;

namespace WPF.Viewmodels.UserVM;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private Page _currentPage;

    [ObservableProperty]
    private Models.User _currentUser;

	public MainViewModel(Models.User user)
    {
		CurrentUser = user;
		CurrentPage = new EventsPageView(this);
	}

    [RelayCommand]
    public void Events()
    {
		CurrentPage = new EventsPageView(this);
	}

	[RelayCommand]
	public void Friends()
    {
		CurrentPage = new FriendsPageView(this);
	}

	[RelayCommand]
	public void Inbox()
	{
		CurrentPage = new InboxPageView(this);
	}

	[RelayCommand]
	public void Settings()
    {
		CurrentPage = new SettingsPageView(this);
	}
}
