using System.Windows;

namespace WPF.Views.UserV;

public partial class MainView : Window
{
	public MainView(Models.User user)
    {
        InitializeComponent();
        DataContext = new Viewmodels.UserVM.MainViewModel(user, this);
	}
}
