using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class EditEventWindowViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }
	private IFileService _fileService { get; init; }
	private INavigationService _navigationService { get; init; }

	private Event _event;
	private byte[] _imageData;

	[ObservableProperty]
	private string _name = "";

	[ObservableProperty]
	private string _description = "";

	[ObservableProperty]
	private string _imageName = "";

	[ObservableProperty]
	private DateTime _date;

	[ObservableProperty]
	private string _location = "";

	[ObservableProperty]
	private string _categoriesInput = "";

	public EditEventWindowViewModel(
		Event @event,
		IDialogService dialogService,
		IWindowService windowService,
		IFileService fileService,
		INavigationService navigationService
	) {
		_dialogService = dialogService;
		_windowService = windowService;
		_fileService = fileService;
		_navigationService = navigationService;

		_event = @event;
		Name = @event.Name;
		Description = @event.Description;
		Date = @event.Date;
		Location = @event.Location;
		CategoriesInput = string.Join(", ", @event.Categories);
		_imageData = @event.ImageData;
	}

	[RelayCommand]
	public async Task Edit()
	{
		if (string.IsNullOrEmpty(Name))
		{
			_dialogService.ShowMessage("Please, enter the name of the event");
			return;
		}
		if (string.IsNullOrEmpty(Description))
		{
			_dialogService.ShowMessage("Please, enter the description of the event");
			return;
		}
		if (Date < DateTime.Today)
		{
			_dialogService.ShowMessage("Please, enter date in the future");
			return;
		}
		if (string.IsNullOrEmpty(Location))
		{
			_dialogService.ShowMessage("Please, enter location");
			return;
		}
		if (CategoriesInput == null)
		{
			_dialogService.ShowMessage("Please, enter categories");
			return;
		}
		if (ImageName == null)  // if ImageName is empty, image won't change
		{
			_dialogService.ShowMessage("Please, select an image");
			return;
		}

		var categories = new ObservableCollection<string>
		(
			CategoriesInput
				.Trim()
				.Split(',')
				.Select(c => c.Trim())
				.Where(c => !string.IsNullOrEmpty(c))
				.ToList()
		);

		_event.Name = Name;
		_event.Description = Description;
		_event.Date = Date;
		_event.ImageData = _imageData;
		_event.Location = Location;
		_event.Categories = categories;

		if (await Managers.EventManager.UpdateEvent(_event))
		{
			_navigationService.NavigateTo<MyEventsPageViewModel>();
			_dialogService.ShowMessage("Event successfully updated");
			return;
		}
		_dialogService.ShowError("There was an error while updating the event. Please try again");
	}

	[RelayCommand]
	public void BrowseImage()
	{
		string filePath = "";
		if (!_windowService.OpenFileDialog("Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png", out filePath))
		{
			_dialogService.ShowError("Error while loading the image. Please try again");
			return;
		}
		if (!_fileService.ReadFile(filePath, ref _imageData))
		{
			_dialogService.ShowError("Error while loading the image. Please try again");
		}
	}
}
