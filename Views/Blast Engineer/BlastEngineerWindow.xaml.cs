using System.Windows;
using WpfApp6.Services;
using WpfApp6.ViewModels;

namespace WpfApp6.Views.Blast_Engineer;

public partial class BlastEngineerWindow : Window
{
    private readonly UserViewModel _userViewModel;

    public BlastEngineerWindow()
    {
        InitializeComponent();
        var currentUser = UserService.Instance.CurrentUser;
        _userViewModel = new UserViewModel();
        DataContext = _userViewModel;
    }

    private void Dashboard_Btn_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to the Dashboard page (implement the page first)
        // MainFrameFromBE.Navigate(new DashboardPage());
    }

    private void Request_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameFromBE.Navigate(new ReportFromBE());
    }

    private void Logout_Btn_Click(object sender, RoutedEventArgs e)
    {
        // Close the current window
        this.Close();

        // Show the login window or perform logout actions
        Application.Current.Shutdown(); // To completely close the application

        // Alternatively, if you want to show the login screen again:
        // var loginWindow = new MainWindow(); // Assuming MainWindow is your login screen
        // loginWindow.Show();
    }
}