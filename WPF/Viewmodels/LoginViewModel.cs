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

	[ObservableProperty]
	private string _username = "";

	[ObservableProperty]
	private SecureString _securePassword = new SecureString();

	[ObservableProperty]
	private string _selectedRole = "Basic User";

	public List<string> Roles { get; } = new() { "Basic User", "Organizer" };

	public LoginViewModel(IDialogService dialogService)
	{
		_dialogService = dialogService;
	}

	private byte[] _receivePasswordHash()
	{
		if (SecurePassword == null)
		{
			_dialogService.ShowError("Please enter another password");
			return Array.Empty<byte>();
		}

		string? plainPassword = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(SecurePassword));

		if (plainPassword == null)
		{
			_dialogService.ShowError("Please enter another password");
			return Array.Empty<byte>();
		}

		using (SHA256 sha256 = SHA256.Create())
		{
			byte[] inputBytes = Encoding.UTF8.GetBytes(plainPassword);
			plainPassword = null;
			byte[] passwordHash = sha256.ComputeHash(inputBytes);
			inputBytes = Array.Empty<byte>();
			return passwordHash;
		}
	}

	[RelayCommand]
	public async Task Login()
	{
		if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(SelectedRole))
		{
			_dialogService.ShowError("Please enter all fields");
			return;
		}

		var hash = _receivePasswordHash();

		if (SelectedRole.Equals("Basic User"))
		{
			if (Username.Equals("admin") && hash.SequenceEqual
				(
					Convert.FromHexString("CA978112CA1BBDCAFAC231B39A23DC4DA786EFF8147C4E72B9807785AFEE48BB")
				))
			{
				var adminWindow = new Views.Admin.MainView();
				adminWindow.Show();
				return;
			}

			User? user = await UserManager.GetUser(Username);

			if (user == null || !user.PasswordHash.SequenceEqual(hash))
			{
				_dialogService.ShowError("Invalid username or password");
				return;
			}

			var windowU = new Views.UserV.MainView(user);
			windowU.Show();
			return;
		}

		// SelectedRole.Equals("Organizer")

		EventOrganizer? eventOrganizer = await EventOrganizerManager
			.GetEventOrganizerByUsername(Username);

		if (eventOrganizer == null || !eventOrganizer.PasswordHash.SequenceEqual(hash))
		{
			_dialogService.ShowError("Invalid username or password");
			return;
		}

		var windowO = new Views.Organizer.MainView(eventOrganizer);
		windowO.Show();
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
			_dialogService.ShowError($"Maximum length has been exceeded by {Username.Length - 30} characters!");
			return;
		}

		if (!SelectedRole.Equals("Basic User"))
		{
			_dialogService.ShowError("You can register only as basic user");
			return;
		}

		var hash = _receivePasswordHash();

		if (await UserManager.ContainsUser(Username))
		{
			_dialogService.ShowError("Username already exists");
			return;
		}

		User user = new User(Username, hash);

		await UserManager.AddUser(user);

		var window = new Views.UserV.MainView(user);
		window.Show();
	}
}
