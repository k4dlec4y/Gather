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
using System.Windows.Shapes;

namespace WPF.Views.User
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
		public MainView()
        {
            InitializeComponent();
			MainFrame.Navigate(new EventsPageView());
		}

        private void Events_Click(object sender, RoutedEventArgs e)
        {
			MainFrame.Navigate(new EventsPageView());
		}

		private void FriendList_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Settings_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new SettingsPageView());
		}

	}
}
