using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.Models;

namespace WPF.Views.User
{
    /// <summary>
    /// Interaction logic for EventView.xaml
    /// </summary>
    public partial class EventDetailsView : Window
    {
		public EventDetailsView(Event selectedEvent)
		{
			InitializeComponent();
			DataContext = selectedEvent;
		}
	}
}
