using System.Windows;

namespace WPF.Views.Admin;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
		DataContext = new Viewmodels.Admin.MainViewModel();
	}
}
