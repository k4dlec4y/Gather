using WPF.Models;

namespace WPF.Services.Abstractions;

internal interface IUserIdentityService
{
	User? CurrentUser { get; }
	bool IsLoggedIn => CurrentUser != null;

	void Login(User user);
	void Logout();
}
