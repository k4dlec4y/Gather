using System.Windows;
using WPF.Services.Abstractions;

namespace WPF.Services.Implementations;

public class WpfDialogService : IDialogService
{
	private void ShowMessageBox(
		string message,
		string title,
		MessageBoxImage image)
	{
		MessageBox.Show(message, title, MessageBoxButton.OK, image);
	}

	public void ShowMessage(string message, string title = "Information")
	{
		ShowMessageBox(message, title, MessageBoxImage.Information);
	}

	public void ShowError(string message, string title = "Error")
	{
		ShowMessageBox(message, title, MessageBoxImage.Error);
	}
}
