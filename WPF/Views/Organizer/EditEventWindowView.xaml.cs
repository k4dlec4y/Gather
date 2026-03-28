using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Models;
using WPF.Viewmodels.Organizer;

namespace WPF.Views.Organizer;

public partial class EditEventWindowView : Window
{
	public EditEventWindowView(Event @event)
	{
		InitializeComponent();
		DataContext = ActivatorUtilities.CreateInstance<EditEventWindowViewModel>(
			App.Current.Services,
			@event
		);
	}
}
