using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WPF.Models;
using WPF.Services.Abstractions;

namespace WPF.Viewmodels.Organizer;

internal partial class CreateEventWindowViewModel : ObservableObject
{
	private IDialogService _dialogService { get; init; }

	private EventOrganizer _eventOrganizer;
	private ObservableCollection<Event> _myEvents;

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
		EventOrganizer eventOrganizer,
		ObservableCollection<Event> myEvents,
		IDialogService dialogService
	) {
		_eventOrganizer = eventOrganizer;
		_myEvents = myEvents;
		_dialogService = dialogService;
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

		var categories = new ObservableCollection<string>
		(
			CategoriesInput
				.Trim()
				.Split(',')
				.Select(c => c.Trim())
				.Where(c => !string.IsNullOrEmpty(c))
				.ToList()
		);

		var newEvent = new Event(
			Name,
			Description,
			Date,
			Location,
			_imageData,
			_eventOrganizer.Id,
			categories
		);

		if (await Managers.EventManager.CreateEvent(newEvent))
		{
			newEvent.Organizer = _eventOrganizer;
			_myEvents.Add(newEvent);
			_dialogService.ShowMessage("Event successfully created");
			return;
		}
		_dialogService.ShowError("There was an error while creating the event. Please try again");
	}

	[RelayCommand]
	public void BrowseImage()
	{
		var openFileDialog = new OpenFileDialog
		{
			Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
		};

		if (openFileDialog.ShowDialog() == true)
		{
			try
			{
				ImageName = openFileDialog.FileName;
				_imageData = File.ReadAllBytes(ImageName);
			}
			catch
			{
				_dialogService.ShowError("Error while loading the image. Please try again");
				return;
			}
		}
	}
}
