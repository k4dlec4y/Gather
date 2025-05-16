using System.Windows;
using System.Windows.Controls;
using WPF.Models;
using WPF.Viewmodels.UserVM;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views.UserV
{
    public partial class FriendsPageView : Page
    {
        public FriendsPageView(MainViewModel main)
        {
			InitializeComponent();
			DataContext = new FriendsPageViewModel(main);
		}
    }
}
