using System.Windows;
using SourceChord.FluentWPF;
using WpfApp6.Services;
using WpfApp6.ViewModels;

namespace WpfApp6.Views.Store_Keeper;

/// <summary>
/// Interaction logic for StoreKeeperWindow.xaml
/// </summary>
public partial class StoreKeeperWindow : AcrylicWindow
{
    private readonly UserViewModel _userViewModel;
    public StoreKeeperWindow()
    {
        InitializeComponent();
        var currentUser = UserService.Instance.CurrentUser;
        _userViewModel = new UserViewModel();
        DataContext = _userViewModel;
    }

    private void Dashboard_Btn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void Request_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameFromSK.Navigate(new RequestFromSK());
    }
}