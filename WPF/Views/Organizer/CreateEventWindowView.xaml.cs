using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.Models;

namespace WPF.Views.Organizer;

public partial class CreateEventWindowView : Window
{
    public CreateEventWindowView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<Viewmodels.Organizer.CreateEventWindowViewModel>();
    }
}
