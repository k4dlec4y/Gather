using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.User;

namespace WPF.Views.UserV;

public partial class FriendsView : UserControl
{
	public FriendsView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<FriendsViewModel>();
	}
}
