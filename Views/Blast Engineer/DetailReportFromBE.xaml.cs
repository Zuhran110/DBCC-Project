using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp6.Views.General_Manager;

namespace WpfApp6.Views.Blast_Engineer;

/// <summary>
/// Interaction logic for DetailReportFromBE.xaml
/// </summary>
public partial class DetailReportFromBE : Page
{
    public Report SelectedReport
    {
        get; set;
    }

    public DetailReportFromBE(Report report)
    {
        InitializeComponent();
        SelectedReport = report;
        DataContext = SelectedReport;
    }

    public DetailReportFromBE()
    {
        InitializeComponent();
    }

    private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}