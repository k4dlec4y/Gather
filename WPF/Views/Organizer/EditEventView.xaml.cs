using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Models;
using WPF.Viewmodels.Organizer;

namespace WPF.Views.Organizer;

public partial class EditEventView : Window
{
	public EditEventView(Event @event)
	{
		InitializeComponent();
		DataContext = ActivatorUtilities.CreateInstance<EditEventViewModel>(
			App.Current.Services,
			@event
		);
	}
}
