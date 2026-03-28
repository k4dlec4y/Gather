using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.User;

namespace WPF.Views.UserV;

public partial class InboxPageView : UserControl
{
	public InboxPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<InboxPageViewModel>();
	}
}
