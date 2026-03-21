using System.Windows;
using WPF.Models;
using WPF.Viewmodels.Organizer;

namespace WPF.Views.Organizer;

public partial class EditEventWindowView : Window
{
    public EditEventWindowView(Event @event, MainViewModel mainVM)
    {
        InitializeComponent();
        DataContext = new EditEventWindowViewModel(@event, mainVM, new Services.Implementations.WpfDialogService());
    }
}
