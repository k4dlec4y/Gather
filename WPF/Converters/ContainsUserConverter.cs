using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using WPF.Models;

namespace WPF.Converters
{
	public class ContainsUserConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ObservableCollection<User> participants && parameter is User user)
			{
				return participants.Contains(user);
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
