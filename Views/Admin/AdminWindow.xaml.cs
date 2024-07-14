using System.Windows;
using SourceChord.FluentWPF;

namespace WpfApp6.Views.Admin;

/// <summary>
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : AcrylicWindow
{
    public AdminWindow()
    {
        InitializeComponent();
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
    }

    private void Empoyees_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameAdmin.Navigate(new EmployeesPageFromAdmin());
    }

    private void Store_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameAdmin.Navigate(new StoresPageFromAdmin());
    }

    private void Project_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameAdmin.Navigate(new ProjectPageFromAdmin());
    }
}