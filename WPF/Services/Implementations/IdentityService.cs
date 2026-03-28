using CommunityToolkit.Mvvm.ComponentModel;
using WPF.Models;
using WPF.Services.Abstractions;

public partial class UserIdentityService : ObservableObject, IUserIdentityService
{
	[ObservableProperty]
	private User? _currentUser;

	public void Login(User user) => CurrentUser = user;
	public void Logout() => CurrentUser = null;
}