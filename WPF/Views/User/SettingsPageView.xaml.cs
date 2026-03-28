using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace WPF.Views.UserV;

public partial class SettingsPageView : UserControl
{
    public SettingsPageView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<Viewmodels.User.SettingsPageViewModel>(); 
    }
}
