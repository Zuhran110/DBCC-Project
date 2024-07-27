using System.ComponentModel;
using System.Windows;
using WpfApp6.Views.Blast_Engineer;
using WpfApp6.Views.Blast_Engineer.ContentDialogue;

namespace WpfApp6.Models;

public class ReportB : INotifyPropertyChanged
{
    private DateTime _date;
    private double _totalEmulsion;
    private double _totalEmulsion65mm;
    private double _totalEmulsion50mm;
    private double _totalAnfo;
    private double _totalVolume;
    private double _averageDepth;
    private double _diameter;
    private double _stemming;
    private double _emulsionPerHole;
    private double _emulsionDensity;
    private double _anfoDensity;
    private double _spacing;
    private double _burden;

    public string SelectedItem
    {
        get; set;
    }

    public double Quantity
    {
        get; set;
    }

    public List<(string SelectedItem, double Quantity)> Materials { get; set; } = new List<(string SelectedItem, double Quantity)>();

    public DateTime Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged(nameof(Date));
        }
    }

    // Implement other properties similarly with OnPropertyChanged
    public double TotalEmulsion
    {
        get => _totalEmulsion;
        set
        {
            _totalEmulsion = value;
            OnPropertyChanged(nameof(TotalEmulsion));
        }
    }

    public double TotalEmulsion65mm
    {
        get => _totalEmulsion65mm;
        set
        {
            _totalEmulsion65mm = value;
            OnPropertyChanged(nameof(TotalEmulsion65mm));
        }
    }

    public double TotalEmulsion50mm
    {
        get => _totalEmulsion50mm;
        set
        {
            _totalEmulsion50mm = value;
            OnPropertyChanged(nameof(TotalEmulsion50mm));
        }
    }

    public double TotalAnfo
    {
        get => _totalAnfo;
        set
        {
            _totalAnfo = value;
            OnPropertyChanged(nameof(TotalAnfo));
        }
    }

    public double TotalVolume
    {
        get => _totalVolume;
        set
        {
            _totalVolume = value;
            OnPropertyChanged(nameof(TotalVolume));
        }
    }

    public double AverageDepth
    {
        get => _averageDepth;
        set
        {
            _averageDepth = value;
            OnPropertyChanged(nameof(AverageDepth));
        }
    }

    public double Diameter
    {
        get => _diameter;
        set
        {
            _diameter = value;
            OnPropertyChanged(nameof(Diameter));
        }
    }

    public double Stemming
    {
        get => _stemming;
        set
        {
            _stemming = value;
            OnPropertyChanged(nameof(Stemming));
        }
    }

    public double EmulsionPerHole
    {
        get => _emulsionPerHole;
        set
        {
            _emulsionPerHole = value;
            OnPropertyChanged(nameof(EmulsionPerHole));
        }
    }

    public double EmulsionDensity
    {
        get => _emulsionDensity;
        set
        {
            _emulsionDensity = value;
            OnPropertyChanged(nameof(EmulsionDensity));
        }
    }

    public double AnfoDensity
    {
        get => _anfoDensity;
        set
        {
            _anfoDensity = value;
            OnPropertyChanged(nameof(AnfoDensity));
        }
    }

    public double Spacing
    {
        get => _spacing;
        set
        {
            _spacing = value;
            OnPropertyChanged(nameof(Spacing));
        }
    }

    public double Burden
    {
        get => _burden;
        set
        {
            _burden = value;
            OnPropertyChanged(nameof(Burden));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ShowAddRequestWindow()
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