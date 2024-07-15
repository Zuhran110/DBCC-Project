using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp6.Views.General_Manager;

namespace WpfApp6.Views.Store_Keeper
{
    /// <summary>
    /// Interaction logic for ok.xaml
    /// </summary>
    public partial class ok : Page
    {
        public Report SelectedReport
        {
            get; set;
        }

        public ok(Report report)
        {
            InitializeComponent();
            SelectedReport = report;
            DataContext = SelectedReport;
        }

        public ok()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Approve_Btn(object sender, RoutedEventArgs e)
        {
            if (SelectedReport != null)
            {
                SelectedReport.RequestStatus = "Accepted";
                // Update the data source if needed and navigate back
                NavigationService.GoBack();
            }
        }

        private void Decline_Btn(object sender, RoutedEventArgs e)
        {
            if (SelectedReport != null)
            {
                SelectedReport.RequestStatus = "Declined";
                // Update the data source if needed and navigate back
                NavigationService.GoBack();
            }
        }
    }
}