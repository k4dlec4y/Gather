using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPF.Viewmodels.User;

namespace WPF.Views.UserV;

public partial class InboxView : UserControl
{
	public InboxView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<InboxViewModel>();
	}
}
