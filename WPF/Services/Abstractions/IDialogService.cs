namespace WPF.Services.Abstractions;

internal interface IDialogService
{
	void ShowMessage(string message, string title = "Information");
	void ShowError(string message, string title = "Error");
}
