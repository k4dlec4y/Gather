using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using WPF.Managers;
using WPF.Models;

namespace WPF.Viewmodels
{
	public partial class LoginViewModel : ObservableObject
	{
		[ObservableProperty]
		private string _username = "";

		[ObservableProperty]
		private SecureString _securePassword = new SecureString();

		[ObservableProperty]
		private string _selectedRole = "Basic User";

		public List<string> Roles { get; } = new() { "Basic User", "Organizer" };

		private byte[] _receivePasswordHash()
		{
			if (SecurePassword == null)
			{
				MessageBox.Show("Please enter another password");
				return Array.Empty<byte>();
			}

			string? plainPassword = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(SecurePassword));

			if (plainPassword == null)
			{
				MessageBox.Show("Please enter another password");
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
				MessageBox.Show("Please enter all fields");
				return;
			}

			var hash = _receivePasswordHash();

			if (SelectedRole.Equals("Basic User"))
			{
				if (Username.Equals("admin") && hash.SequenceEqual(Convert.FromHexString("CA978112CA1BBDCAFAC231B39A23DC4DA786EFF8147C4E72B9807785AFEE48BB")))
				{
					var adminWindow = new Views.Admin.MainView();
					adminWindow.Show();
					return;
				}

				User? user = await UserManager.GetUser(Username);

				if (user == null || !user.PasswordHash.SequenceEqual(hash))
				{
					MessageBox.Show("Invalid username or password");
					return;
				}

				var windowU = new Views.UserV.MainView(user);
				windowU.Show();
				return;
			}

			// SelectedRole.Equals("Organizer")

			EventOrganizer? eventOrganizer = await EventOrganizerManager.GetEventOrganizerByUsername(Username);

			if (eventOrganizer == null || !eventOrganizer.PasswordHash.SequenceEqual(hash))
			{
				MessageBox.Show("Invalid username or password");
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
				MessageBox.Show("Please enter all fields");
				return;
			}

			if (Username.Length > 30)
			{
				MessageBox.Show($"Maximum length has been exceeded by {Username.Length - 30} characters!");
				return;
			}

			if (!SelectedRole.Equals("Basic User"))
			{
				MessageBox.Show("You can register only as basic user");
				return;
			}

			var hash = _receivePasswordHash();

			if (await UserManager.ContainsUser(Username))
			{
				MessageBox.Show("Username already exists");
				return;
			}

			Models.User user = new Models.User(Username, hash);

			await UserManager.AddUser(user);

			var window = new Views.UserV.MainView(user);
			window.Show();
		}
	}
}
