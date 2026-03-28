using System.Windows.Controls;
using WPF.Viewmodels.User;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views.UserV;

public partial class FriendsPageView : UserControl
{
    public FriendsPageView()
    {
			InitializeComponent();
			DataContext = App.Current.Services.GetService<FriendsPageViewModel>();
		}
}
