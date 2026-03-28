using System.Windows;
using WPF.Services.Abstractions;

namespace WPF.Services.Implementations;

internal class WpfDialogService : IDialogService
{
	private Window? GetActiveWindow()
	{
		var active = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
		if (active == null)
		{
			active = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsVisible);
		}
		return active ?? Application.Current.MainWindow;
	}

	private void ShowMessageBox(
		string message,
		string title,
		MessageBoxImage image)
	{
		var owner = GetActiveWindow();
		MessageBox.Show(owner!, message, title, MessageBoxButton.OK, image);
		owner?.Activate();
		owner?.Focus();
	}

	public void ShowMessage(string message, string title = "Information")
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			ShowMessageBox(message, title, MessageBoxImage.Information);
		});
	}

	public void ShowError(string message, string title = "Error")
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			ShowMessageBox(message, title, MessageBoxImage.Error);
		});
	}
}
