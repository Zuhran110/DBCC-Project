using System.Windows;
using SourceChord.FluentWPF;

namespace WpfApp6.Views.General_Manager;

/// <summary>
/// Interaction logic for GeneralManagerWindow.xaml
/// </summary>
public partial class GeneralManagerWindow : AcrylicWindow
{
    public GeneralManagerWindow()
    {
        InitializeComponent();
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