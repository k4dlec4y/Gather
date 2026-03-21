using System.Windows.Controls;
using WPF.Viewmodels.UserVM;

namespace WPF.Views.UserV
{
    public partial class FriendsPageView : Page
    {
        public FriendsPageView(MainViewModel main)
        {
			InitializeComponent();
			DataContext = new FriendsPageViewModel(main, new Services.Implementations.WpfDialogService());
		}
    }
}
