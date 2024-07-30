using System.Windows;
using SourceChord.FluentWPF;
using WpfApp6.Services;
using WpfApp6.ViewModels;

namespace WpfApp6.Views.General_Manager;

/// <summary>
/// Interaction logic for GeneralManagerWindow.xaml
/// </summary>
public partial class GeneralManagerWindow : AcrylicWindow
{
    private readonly UserViewModel _userViewModel;
    public GeneralManagerWindow()
    {
        InitializeComponent();
        var currentUser = UserService.Instance.CurrentUser;
        _userViewModel = new UserViewModel();
        DataContext = _userViewModel;
    }

    private void Dashboard_Btn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void Employees_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameFromGM.Navigate(new EmployeeFromGM());
    }

    private void Stores_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameFromGM.Navigate(new StoreFromGM());
    }

    private void Projects_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameFromGM.Navigate(new ProjectFromGM());
    }

    private void Report_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameFromGM.Navigate(new ReportFromGM());
    }
}