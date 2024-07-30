using System.Windows;
using WpfApp6.Models;
using WpfApp6.Services;

namespace WpfApp6.Views.Blast_Engineer
{
    public partial class ReportDetails : Window
    {
        private ReportB _report;
        private string _projectName;
        private string _storeName;

        public ReportDetails(ReportB report, string projectName, string storeName)
        {
            InitializeComponent();
            _report = report;
            _projectName = projectName;
            _storeName = storeName;

            var currentUser = UserService.Instance.CurrentUser;
            if (currentUser != null)
            {
                // Use currentUser info as needed
            }

            LoadReportDetails();
        }

        private void LoadReportDetails()
        {
            // Populate text blocks with report data
            TotalEmulsionTextBlock.Text = $"Total Emulsion: {_report.TotalEmulsion:F2}";
            TotalAnfoTextBlock.Text = $"Total Anfo: {_report.TotalAnfo:F2}";
            TotalVolumeTextBlock.Text = $"Total Volume: {_report.TotalVolume:F2}";
            CreatedByTextBlock.Text = $"Created by: {_report.CreatedBy}";
            CreatedDateTextBlock.Text = $"on: {_report.Date.ToString("yyyy-MM-dd HH:mm:ss")}";
            ProjectNameTextBlock.Text = $"Project: {_projectName}";
            StoreNameTextBlock.Text = $"Store: {_storeName}";

            // Populate materials list view manually
            MaterialsListView.Items.Clear();
            foreach (var material in _report.Materials)
            {
                var item = new MaterialItem
                {
                    Material = material.SelectedItem,
                    Quantity = material.Quantity
                };
                MaterialsListView.Items.Add(item);
            }
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Implement submit logic here
            MessageBox.Show("Submitted successfully!", "Submit", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            // Implement cancel logic here
            this.Close();
        }
    }
}