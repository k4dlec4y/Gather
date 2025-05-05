using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using WPF.Models;

namespace WPF.Viewmodels.Organizer;

public partial class CreateEventWindowViewModel : ObservableObject
{
	private EventOrganizer _eventOrganizer;
	private readonly string _dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
	private ObservableCollection<Event> _myEvents;

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
		Date = DateTime.Now;
		_myEvents = myEvents;
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
		if (ImageName.IsNullOrEmpty())
		{
			MessageBox.Show("Please, enter the image path");
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

		var categories = CategoriesInput.Trim().Split(',')
			.Select(c => c.Trim())
			.Where(c => !string.IsNullOrEmpty(c))
			.ToList();

		var newEvent = new Event
		{
			Name = Name,
			Description = Description,
			ImageName = Path.GetFileName(ImageName),
			Date = Date,
			Location = Location,
			OrganizerId = _eventOrganizer.Id,
			Categories = new ObservableCollection<string>(categories)
		};

		if (await Managers.EventManager.CreateEvent(newEvent))
		{
			MessageBox.Show("Event successfully created");
			_myEvents.Add(newEvent);
			return;
		}
		MessageBox.Show("There was an error while creating the event. Please try again later");
	}

	[RelayCommand]
	public void BrowseImage()
	{
		var openFileDialog = new Microsoft.Win32.OpenFileDialog
		{
			Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
		};

		if (openFileDialog.ShowDialog() == true)
		{
			string sourcePath = openFileDialog.FileName;
			string fileName = Path.GetFileName(sourcePath);
			string destinationPath = Path.Combine(_dataFolderPath, fileName);

			try
			{
				if (File.Exists(destinationPath))
				{
					throw new IOException($"File {fileName} already exists in the destination folder.");
				}

				File.Copy(sourcePath, destinationPath);
				ImageName = fileName;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error copying image: {ex.Message}",
					"Error",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
			}
		}
	}
}
