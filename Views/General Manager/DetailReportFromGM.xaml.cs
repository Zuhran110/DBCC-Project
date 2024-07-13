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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp6.Views.General_Manager;

/// <summary>
/// Interaction logic for DetailReportFromGM.xaml
/// </summary>
public partial class DetailReportFromGM : Page
{
    public Report SelectedReport
    {
        get; set;
    }

    public DetailReportFromGM(Report report)
    {
        InitializeComponent();
        SelectedReport = report;
        DataContext = SelectedReport;
    }

    public DetailReportFromGM()
    {
        InitializeComponent();
    }

    private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}