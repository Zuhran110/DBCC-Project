using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp6.Models;
using WpfApp6.Services;
using WpfApp6.Views.Blast_Engineer;
using WpfApp6.Views.Blast_Engineer.ContentDialogue;

namespace WpfApp6.ViewModels.Blast_Engineer;

public partial class ReportWindowViewModel : ObservableObject
{
    public List<ReportB> Reports => SharedData.Instance.Reports;

    [ObservableProperty]
    private double totalSumEmulsion;

    [ObservableProperty]
    private double totalSumEmulsion65mm;

    [ObservableProperty]
    private double totalSumEmulsion50mm;

    [ObservableProperty]
    private double totalSumAnfo;

    [ObservableProperty]
    private double totalSumVolume;

    [ObservableProperty]
    private string currentUserName;

    public ReportWindowViewModel()
    {
        CalculateTotals();
        var currentUser = UserService.Instance.CurrentUser;
        if (currentUser != null)
        {
            CurrentUserName = $"Logged in as: {currentUser.UserName}";
        }
    }

    [RelayCommand]
    public void BackToDashboard()
    {
        // Implement navigation to dashboard
    }

    [RelayCommand]
    public void AddCalculation()
    {
        var addRequestWindow = new AddRequestWindow();
        bool? result = addRequestWindow.ShowDialog();

        if (result == true)
        {
            int numberOfColumns = addRequestWindow.NumberOfColumns;
            int totalHoles = addRequestWindow.NumberOfHoles;

            var enterDepthsWindow = new EnterDepthsWindow(totalHoles, numberOfColumns);
            bool? depthsResult = enterDepthsWindow.ShowDialog();

            if (depthsResult == true)
            {
                // Retrieve and calculate values
                double diameter = double.Parse(enterDepthsWindow.DiameterTextBox.Text);
                double stemming = double.Parse(enterDepthsWindow.StemmingTextBox.Text);
                double emulsion65mmPerHole = double.Parse(enterDepthsWindow.Emulsion65mmPerHoleTextBox.Text);
                double emulsion50mmPerHole = double.Parse(enterDepthsWindow.Emulsion50mmPerHoleTextBox.Text);
                double emulsionDensity = double.Parse(enterDepthsWindow.EmulsionDensityTextBox.Text);
                double anfoDensity = double.Parse(enterDepthsWindow.AnfoDensityTextBox.Text);
                double spacing = double.Parse(enterDepthsWindow.SpacingTextBox.Text);
                double burden = double.Parse(enterDepthsWindow.BurdenTextBox.Text);

                double averageDepth = enterDepthsWindow.CalculateAverageDepth();
                double totalEmulsion65mm = enterDepthsWindow.CalculateTotalEmulsion65mm(emulsion65mmPerHole, totalHoles);
                double totalEmulsion50mm = enterDepthsWindow.CalculateTotalEmulsion50mm(emulsion50mmPerHole, totalHoles);
                double totalEmulsion = totalEmulsion65mm + totalEmulsion50mm;
                double totalVolume = enterDepthsWindow.CalculateTotalVolume(averageDepth, spacing, burden);
                double totalAnfo = enterDepthsWindow.CalculateTotalAnfo(totalVolume, totalHoles);

                var newReport = new ReportB
                {
                    Date = DateTime.Now,
                    TotalEmulsion = totalEmulsion,
                    TotalEmulsion65mm = totalEmulsion65mm,
                    TotalEmulsion50mm = totalEmulsion50mm,
                    TotalAnfo = totalAnfo,
                    TotalVolume = totalVolume,
                    AverageDepth = averageDepth,
                    Diameter = diameter,
                    Stemming = stemming,
                    EmulsionDensity = emulsionDensity,
                    AnfoDensity = anfoDensity,
                    Spacing = spacing,
                    Burden = burden
                };

                SharedData.Instance.Reports.Add(newReport);
                CalculateTotals();
            }
        }
        else
        {
            MessageBox.Show("Request Cancelled", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void CalculateTotals()
    {
        TotalSumEmulsion = Reports.Sum(r => r.TotalEmulsion);
        TotalSumEmulsion65mm = Reports.Sum(r => r.TotalEmulsion65mm);
        TotalSumEmulsion50mm = Reports.Sum(r => r.TotalEmulsion50mm);
        TotalSumAnfo = Reports.Sum(r => r.TotalAnfo);
        TotalSumVolume = Reports.Sum(r => r.TotalVolume);
    }
}