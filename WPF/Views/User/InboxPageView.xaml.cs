using System.Windows.Controls;
using WPF.Viewmodels.UserVM;

namespace WPF.Views.UserV
{
    public partial class InboxPageView : Page
    {
        public InboxPageView(MainViewModel main)
        {
            InitializeComponent();
            DataContext = new InboxPageViewModel(main);
		}
    }
}
