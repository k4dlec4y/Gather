using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace WPF.Views.Organizer;

public partial class CreateEventView : Window
{
	public CreateEventView()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetRequiredService<Viewmodels.Organizer.CreateEventViewModel>();
	}
}
