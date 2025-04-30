using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using WPF.Views.UserV;

namespace WPF.Viewmodels.UserVM
{
    public partial class MainViewModel : ObservableObject
	{
        [ObservableProperty]
        private Page _currentPage;

        [ObservableProperty]
        private Models.User _user;

        public MainViewModel(Models.User user)
        {
            CurrentPage = new EventsPageView(this);
            User = user;
        }

        [RelayCommand]
        public void Events()
        {
			CurrentPage = new EventsPageView(this);
		}

		[RelayCommand]
		public void FriendList()
        {

        }


		[RelayCommand]
		public void Settings()
        {
			CurrentPage = new SettingsPageView(this);
		}
	}
}
