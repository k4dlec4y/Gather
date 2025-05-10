using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WPF.Models;

namespace WPF.Viewmodels.Organizer;

public partial class CreateEventWindowViewModel : ObservableObject
{
	private EventOrganizer _eventOrganizer;
	private ObservableCollection<Event> _myEvents;
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

	public CreateEventWindowViewModel(EventOrganizer eventOrganizer, ObservableCollection<Event> myEvents)
	{
		_eventOrganizer = eventOrganizer;
		_myEvents = myEvents;
		Date = DateTime.Now;
	}

	[RelayCommand]
	public async Task Create()
	{
		if (Name.IsNullOrEmpty())
		{
			MessageBox.Show("Please, enter the name of the event");
			return;
		}
		if (Description.IsNullOrEmpty())
		{
			MessageBox.Show("Please, enter the description of the event");
			return;
		}
		if (Date < DateTime.Today)
		{
			MessageBox.Show("Please, enter valid date");
			return;
		}
		if (Location.IsNullOrEmpty())
		{
			MessageBox.Show("Please, enter location");
			return;
		}
		if (CategoriesInput == null)
		{
			MessageBox.Show("Please, enter categories");
			return;
		}
		if (string.IsNullOrEmpty(ImageName))
		{
			MessageBox.Show("Please, select an image");
			return;
		}

		var categories = new ObservableCollection<string>
		(
			CategoriesInput.Trim().Split(',')
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
			MessageBox.Show("Event successfully created");
			newEvent.Organizer = _eventOrganizer;
			_myEvents.Add(newEvent);
			return;
		}
		MessageBox.Show("There was an error while creating the event. Please try again");
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
				MessageBox.Show("Error while loading the image. Please try again");
				return;
			}
		}
	}
}
