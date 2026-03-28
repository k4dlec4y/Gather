using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using WPF.Managers;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels;

internal partial class LoginViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }

	[ObservableProperty]
	private string _username = "";

	[ObservableProperty]
	private SecureString _securePassword = new SecureString();
	private byte[] _passwordHash = Array.Empty<byte>();

	[ObservableProperty]
	private string _selectedRole = "Basic User";

	public List<string> Roles { get; } = new() { "Basic User", "Organizer" };

	public LoginViewModel(
		IDialogService dialogService,
		IWindowService windowService
	) {
		_dialogService = dialogService;
		_windowService = windowService;
	}

	private bool _computePasswordHash()
	{
		if (SecurePassword == null)
			return false;

		string? plainPassword = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(SecurePassword));
		if (plainPassword == null)
			return false;

		using (SHA256 sha256 = SHA256.Create())
		{
			_passwordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
			plainPassword = null;
		}
		return true;
	}

	[RelayCommand]
	public async Task Login()
	{
		if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(SelectedRole))
		{
			_dialogService.ShowError("Please enter all fields");
			return;
		}

		if (!_computePasswordHash())
		{
			_dialogService.ShowError("Please enter another password");
			return;
		}

		if (SelectedRole.Equals("Basic User"))
		{
			if (Username.Equals("admin") &&
				_passwordHash.SequenceEqual(Convert.FromHexString("CA978112CA1BBDCAFAC231B39A23DC4DA786EFF8147C4E72B9807785AFEE48BB"))
			) {
				_windowService.OpenMainAdminWindow(new Models.User("admin", Array.Empty<byte>()));
				return;
			}

			Models.User? user = await UserManager.GetUser(Username);
			if (user == null || !user.PasswordHash.SequenceEqual(_passwordHash))
			{
				_dialogService.ShowError("Invalid username or password");
				return;
			}
			_windowService.OpenMainUserWindow(user);
			return;
		}

		EventOrganizer? eventOrganizer = await EventOrganizerManager.GetEventOrganizerByUsername(Username);
		if (eventOrganizer == null || !eventOrganizer.PasswordHash.SequenceEqual(_passwordHash))
		{
			_dialogService.ShowError("Invalid username or password");
			return;
		}

		_windowService.OpenMainOrganizerWindow(eventOrganizer);
		return;
	}

	[RelayCommand]
	public async Task Register()
	{
		if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(SelectedRole))
		{
			_dialogService.ShowError("Please enter all fields");
			return;
		}

		if (Username.Length > 30)
		{
			_dialogService.ShowError($"Maximum username size has been exceeded by {Username.Length - 30} characters!");
			return;
		}

		if (!SelectedRole.Equals("Basic User"))
		{
			_dialogService.ShowError("You can register only as basic user");
			return;
		}

		if (!_computePasswordHash())
		{
			_dialogService.ShowError("Please enter another password");
			return;
		}

		if (await UserManager.ContainsUser(Username))
		{
			_dialogService.ShowError("Username already exists");
			return;
		}

		Models.User user = new Models.User(Username, _passwordHash);
		await UserManager.AddUser(user);

		_windowService.OpenMainUserWindow(user);
	}

	[RelayCommand]
	public void Exit()
	{
		// _windowService.CloseAllWindows();
	}
}
