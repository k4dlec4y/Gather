using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF.Views
{
    /// <summary>
    /// Interaction logic for UserMainView.xaml
    /// </summary>
	public partial class UserMainWindowView : Window
	{
		public UserMainWindowView()
		{
			InitializeComponent();
			MainContentArea.Content = new HomeView(); // default view
		}

		private void Home_Click(object sender, RoutedEventArgs e)
		{
			MainContentArea.Content = new HomeView();
		}

		private void FriendList_Click(object sender, RoutedEventArgs e)
		{
			MainContentArea.Content = new FriendListView();
		}

		private void Settings_Click(object sender, RoutedEventArgs e)
		{
			MainContentArea.Content = new SettingsView();
		}
	}
}

