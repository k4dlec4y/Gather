using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WPF.Models;

namespace WPF.Viewmodels.Organizer;

public partial class EditEventWindowViewModel : ObservableObject
{
	private Event _event;
	private byte[] _imageData;
	private MainViewModel _mainVM;

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

	public EditEventWindowViewModel(Event @event, MainViewModel mainVM)
	{
		_event = @event;
		Name = @event.Name;
		Description = @event.Description;
		Date = @event.Date;
		Location = @event.Location;
		CategoriesInput = string.Join(", ", @event.Categories);
		_imageData = @event.ImageData;

		_mainVM = mainVM;
	}

	[RelayCommand]
	public async Task Edit()
	{
		if (string.IsNullOrEmpty(Name))
		{
			MessageBox.Show("Please, enter the name of the event");
			return;
		}
		if (string.IsNullOrEmpty(Description))
		{
			MessageBox.Show("Please, enter the description of the event");
			return;
		}
		if (Date < DateTime.Today)
		{
			MessageBox.Show("Please, enter date in the future");
			return;
		}
		if (string.IsNullOrEmpty(Location))
		{
			MessageBox.Show("Please, enter location");
			return;
		}
		if (CategoriesInput == null)
		{
			MessageBox.Show("Please, enter categories");
			return;
		}
		if (ImageName == null)  // if ImageName is empty, image won't change
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

		_event.Name = Name;
		_event.Description = Description;
		_event.Date = Date;
		_event.ImageData = _imageData;
		_event.Location = Location;
		_event.Categories = categories;

		if (await Managers.EventManager.UpdateEvent(_event))
		{
			_mainVM.CurrentPage = new Views.Organizer.MyEventsPageView(_mainVM);
			MessageBox.Show("Event successfully updated");
			return;
		}
		MessageBox.Show("There was an error while updating the event. Please try again");
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
