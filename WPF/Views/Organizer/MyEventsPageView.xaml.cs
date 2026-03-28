using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace WPF.Views.Organizer;

public partial class MyEventsPageView : UserControl
{
	public MyEventsPageView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<Viewmodels.Organizer.MyEventsPageViewModel>();
	}
}
