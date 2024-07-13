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