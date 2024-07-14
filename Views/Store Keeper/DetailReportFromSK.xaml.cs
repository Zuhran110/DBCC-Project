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
using WpfApp6.Views.General_Manager;

namespace WpfApp6.Views.Store_Keeper;

/// <summary>
/// Interaction logic for DetailReportFromSK.xaml
/// </summary>
public partial class DetailReportFromSK : Page
{
    public Report SelectedReport
    {
        get; set;
    }

    public DetailReportFromSK(Report report)
    {
        InitializeComponent();
        SelectedReport = report;
        DataContext = SelectedReport;
    }

    public DetailReportFromSK()
    {
        InitializeComponent();
    }

    private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}