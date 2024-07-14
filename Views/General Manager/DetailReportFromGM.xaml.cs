using System.Windows.Controls;
using System.Windows.Navigation;

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