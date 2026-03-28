using System.Windows;
using WPF.Services.Abstractions;

namespace WPF.Services.Implementations;

internal class WpfDialogService : IDialogService
{
	public void ShowMessage(string message, string title = "Information")
	{
		MessageBox.Show
		(
			Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
			message,
			title,
			MessageBoxButton.OK, MessageBoxImage.Information
		);
	}

	public void ShowError(string message, string title = "Error")
	{
		MessageBox.Show
		(
			Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
			message,
			title,
			MessageBoxButton.OK,
			MessageBoxImage.Error
		);
	}
}
