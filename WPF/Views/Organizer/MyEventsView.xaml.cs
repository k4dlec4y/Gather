using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace WPF.Views.Organizer;

public partial class MyEventsView : UserControl
{
	public MyEventsView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService<Viewmodels.Organizer.MyEventsViewModel>();
	}
}
