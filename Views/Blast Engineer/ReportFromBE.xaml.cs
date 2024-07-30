using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Services;
using WpfApp6.Views.Blast_Engineer.ContentDialogue;
using WpfApp6.Views.General_Manager;

namespace WpfApp6.Views.Blast_Engineer;

/// <summary>
/// Interaction logic for ReportFromBE.xaml
/// </summary>
public partial class ReportFromBE : Page
{
    public ObservableCollection<Report> Reports
    {
        get; set;
    }

    private ObservableCollection<Report> _allReports;
    private string _searchText;
    private DateTime? _startDate;
    private DateTime? _endDate;

    public event PropertyChangedEventHandler PropertyChanged;

    public ReportFromBE()
    {
        InitializeComponent();
        DataContext = this;

        // Initialize sample data
        _allReports = new ObservableCollection<Report>
            {
                new Report { BlastLocation = "Mountain A", Date = new DateTime(2024, 1, 1), ExplosiveType = "Dynamite", Status = "Completed",RequestStatus="Pending", BlastingEngineer = "Engineer A", TotalANFO = 500, TotalHoles = 10, TotalEmulsion = 300 },
                new Report { BlastLocation = "Mountain d", Date = new DateTime(2024, 3, 1), ExplosiveType = "ANFO", Status = "Scheduled",RequestStatus="Pending", BlastingEngineer = "Engineer B", TotalANFO = 700, TotalHoles = 15, TotalEmulsion = 400 }
            };

        Reports = new ObservableCollection<Report>(_allReports);
        ReportsDataGrid.ItemsSource = Reports;
        var currentUser = UserService.Instance.CurrentUser;
        if (currentUser != null)
        {
            // Use currentUser info as needed
            UserNameTextBlock.Text = $"Logged in as: {currentUser.UserName}";
        }
    }

    private void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            FilterReports();
        }
    }

    public DateTime? StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged();
            FilterReports();
        }
    }

    public DateTime? EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged();
            FilterReports();
        }
    }

    private void FilterReports()
    {
        var filteredReports = _allReports.Where(report =>
            (string.IsNullOrEmpty(SearchText) || report.BlastLocation.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) &&
            (!StartDate.HasValue || report.Date >= StartDate.Value) &&
            (!EndDate.HasValue || report.Date <= EndDate.Value)).ToList();

        Reports.Clear();
        foreach (var report in filteredReports)
        {
            Reports.Add(report);
        }
    }

    private void ReportDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender == StartDatePicker)
        {
            StartDate = StartDatePicker.SelectedDate;
        }
        else if (sender == EndDatePicker)
        {
            EndDate = EndDatePicker.SelectedDate;
        }
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchText = ((TextBox)sender).Text;
    }

    private void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        FilterReports();
    }

    private void ViewButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (ReportsDataGrid.SelectedItem is Report selectedReport)
        {
        }
    }

    private void ReportsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Handle selection changed event if needed
    }

    private void AddRequest_Click(object sender, RoutedEventArgs e)
    {
        AddRequestWindow addRequestWindow = new AddRequestWindow();
        bool? result = addRequestWindow.ShowDialog(); // Ensure ShowDialog is used

        if (result == true)
        {
            int numberOfColumns = addRequestWindow.NumberOfColumns;
            int totalHoles = addRequestWindow.NumberOfHoles;
            EnterDepthsWindow enterDepthsWindow = new EnterDepthsWindow(totalHoles, numberOfColumns);
            bool? depthsResult = enterDepthsWindow.ShowDialog();

            if (depthsResult == true)
            {
                // Handle successful depth entry
            }
        }
        else
        {
            // Handle the cancellation here
            MessageBox.Show("Request Cancelled", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}