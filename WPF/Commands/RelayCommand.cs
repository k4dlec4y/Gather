using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.Commands
{
	class RelayCommand(Action<object?> executeAction, Predicate<object?> canExecutePredicate) : ICommand
	{
		public event EventHandler? CanExecuteChanged;
		private Action<object?> _executeAction = executeAction;
		private Predicate<object?> _canExecutePredicate = canExecutePredicate;

		public bool CanExecute(object? parameter) => _canExecutePredicate(parameter);

		public void Execute(object? parameter) => _executeAction(parameter);

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
