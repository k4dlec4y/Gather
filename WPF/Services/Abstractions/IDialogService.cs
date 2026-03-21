using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Purpose: Showing alerts, warnings, or asking for simple user input (Yes/No).

Methods: ShowMessage(), ShowWarning(), AskConfirmation().

Used by: Almost every ViewModel.
 */

namespace WPF.Services.Abstractions;

internal interface IDialogService
{
	void ShowMessage(string message, string title = "Information");
	void ShowError(string message, string title = "Error");
}
