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
    }

    private void Projects_Btn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void Report_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameFromGM.Navigate(new ReportFromGM());
    }
}