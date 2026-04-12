namespace WPF.Services.Abstractions;

public interface IDialogService
{
	void ShowMessage(string message, string title = "Information");
	void ShowError(string message, string title = "Error");
}
