using System.Windows;

namespace WpfApp6.Views.Blast_Engineer
{
    public partial class ReportDetails : Window
    {
        public ReportDetails()
        {
            InitializeComponent();
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