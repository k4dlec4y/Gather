using System.Windows;

namespace WPF.Views.UserV
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
		public MainView(Models.User user)
        {
            InitializeComponent();
            DataContext = new Viewmodels.UserVM.MainViewModel(user);
		}
	}
}
