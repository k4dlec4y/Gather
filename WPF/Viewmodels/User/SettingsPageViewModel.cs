using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF.Viewmodels.UserVM
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private MainViewModel _mainVM;

        public SettingsPageViewModel(MainViewModel main)
        {
            MainVM = main;
        }
    }
}
