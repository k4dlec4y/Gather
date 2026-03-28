using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.User;

namespace WPF.Views.UserV;

public partial class FriendsPageView : UserControl
{
	public FriendsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<FriendsPageViewModel>();
	}
}
