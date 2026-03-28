using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WPF.Managers;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class CreateEventWindowViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }
	private IWindowService _windowService { get; init; }
	private IFileService _fileService { get; init; }

	private EventOrganizer _currentEventOrganizer;

	[ObservableProperty]
	private string _name = "";
	[ObservableProperty]
	private string _description = "";
	[ObservableProperty]
	private string _imageName = "";
	private byte[] _imageData = Array.Empty<byte>();
	[ObservableProperty]
	private DateTime _date;
	[ObservableProperty]
	private string _location = "";
	[ObservableProperty]
	private string _categoriesInput = "";

	public CreateEventWindowViewModel(
		IOrganizerIdentityService organizerIdentityService,
		IDialogService dialogService,
		IWindowService windowService,
		IFileService fileService)
	{
		Debug.Assert(
			organizerIdentityService.CurrentEventOrganizer != null,
			"Current event organizer cannot be null when initializing CreateEventWindowViewModel");
		_currentEventOrganizer = organizerIdentityService.CurrentEventOrganizer;

		_dialogService = dialogService;
		_windowService = windowService;
		_fileService = fileService;
		Date = DateTime.Now;
	}

	[RelayCommand]
	public async Task Create()
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
		if (string.IsNullOrEmpty(ImageName))
		{
			_dialogService.ShowMessage("Please, select an image");
			return;
		}

		var categories = new ObservableCollection<string>(
			CategoriesInput
				.Trim()
				.Split(',')
				.Select(c => c.Trim())
				.Where(c => !string.IsNullOrEmpty(c))
				.ToList());

		var newEvent = new Event(
			Name,
			Description,
			Date,
			Location,
			_imageData,
			_currentEventOrganizer.Id,
			categories);

		if (await EventManager.CreateEvent(newEvent))
		{
			newEvent.Organizer = _currentEventOrganizer;
			_currentEventOrganizer.Events.Add(newEvent);
			_dialogService.ShowMessage("Event successfully created");
			return;
		}
		_dialogService.ShowError("There was an error while creating the event. Please try again");
	}

	[RelayCommand]
	public void BrowseImage()
	{
		string filePath = "";
		if (!_windowService.OpenFileDialog(
				"Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
				out filePath))
		{
			_dialogService.ShowError("Error while loading the image. Please try again");
			return;
		}
		ImageName = filePath;

		if (!_fileService.ReadFile(filePath, ref _imageData))
		{
			_dialogService.ShowError("Error while loading the image. Please try again");
		}
	}
}
