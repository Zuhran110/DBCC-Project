using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp6.Views.General_Manager;

namespace WpfApp6.Views.Store_Keeper;

/// <summary>
/// Interaction logic for RequestFromSK.xaml
/// </summary>
public partial class RequestFromSK : Page, INotifyPropertyChanged
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

    public RequestFromSK()
    {
        InitializeComponent();
        DataContext = this;

        // Initialize sample data
        _allReports = new ObservableCollection<Report>
            {
                new Report { BlastLocation = "Mountain A", Date = new DateTime(2024, 1, 1), ExplosiveType = "Dynamite", Status = "Completed",RequestStatus="Approved", BlastingEngineer = "Engineer A", TotalANFO = 500, TotalHoles = 10, TotalEmulsion = 300 },
                new Report { BlastLocation = "Mountain d", Date = new DateTime(2024, 3, 1), ExplosiveType = "ANFO", Status = "Scheduled",RequestStatus="Declined", BlastingEngineer = "Engineer B", TotalANFO = 700, TotalHoles = 15, TotalEmulsion = 400 }
            };

        Reports = new ObservableCollection<Report>(_allReports);
        ReportsDataGrid.ItemsSource = Reports;
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
            var detailPagefromSK = new ok(selectedReport);
            NavigationService.Navigate(detailPagefromSK);
        }
    }

    private void ReportsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Handle selection changed event if needed
    }
}