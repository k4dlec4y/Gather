using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF.Viewmodels.Organizer;

public partial class MyEventsPageViewModel : ObservableObject
{
	[ObservableProperty]
	private MainViewModel _mainVM;

	public MyEventsPageViewModel(MainViewModel mainVM)
	{
		MainVM = mainVM;
	}
}
