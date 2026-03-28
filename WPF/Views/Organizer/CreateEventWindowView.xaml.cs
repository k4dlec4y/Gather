using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace WPF.Views.Organizer;

public partial class CreateEventWindowView : Window
{
	public CreateEventWindowView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetRequiredService<Viewmodels.Organizer.CreateEventWindowViewModel>();
	}
}
