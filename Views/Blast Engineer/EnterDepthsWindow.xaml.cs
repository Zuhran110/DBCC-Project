using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp6.Models;

namespace WpfApp6.Views.Blast_Engineer
{
    public class DepthEntry : System.ComponentModel.INotifyPropertyChanged
    {
        public DepthEntry(int numberOfColumns)
        {
            Depths = new ObservableCollection<double>(new double[numberOfColumns]);
            Depths.CollectionChanged += Depths_CollectionChanged;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<double> Depths
        {
            get; private set;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        private void Depths_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Depths));
        }
    }

    public partial class EnterDepthsWindow : Window
    {
        private int _numberOfColumns;

        private int _numberOfRows;

        private int _totalHoles;

        public EnterDepthsWindow(int numberOfHoles, int numberOfColumns)
        {
            InitializeComponent();
            _totalHoles = numberOfHoles;
            _numberOfColumns = numberOfColumns;
            _numberOfRows = (int)Math.Ceiling((double)numberOfHoles / numberOfColumns);
            TotalHolesTextBlock.Text = $"Total Holes: {_totalHoles}";
            CreateDepthGrid(_numberOfRows, _numberOfColumns);
            UpdateAverageDepth();
        }

        public ObservableCollection<DepthEntry> DepthEntries
        {
            get; private set;
        }

        private double CalculateAnfoCoveringSpace(double anfoPerMeter, double remainingSpace)
        {
            double result = anfoPerMeter * remainingSpace;
            MessageBox.Show($"CalculateAnfoCoveringSpace: AnfoPerMeter={anfoPerMeter}, RemainingSpace={remainingSpace}, Result={result}");
            return result;
        }

        private double CalculateAnfoPerMeter(double diameter, double density)
        {
            double result = (22.0 / 7000.0) * Math.Pow((diameter / 2.0), 2) * density;
            MessageBox.Show($"CalculateAnfoPerMeter: Diameter={diameter}, Density={density}, Result={result}");
            return result;
        }

        public double CalculateAverageDepth()
        {
            double totalDepth = CalculateTotalDepth();
            int totalCount = CalculateTotalCount();
            return totalCount > 0 ? totalDepth / totalCount : 0.0;
        }

        public double CalculateEmulsionCoveringSpace(double emulsionPerHole, double emulsionPerMeter)
        {
            double result = emulsionPerHole / emulsionPerMeter;
            MessageBox.Show($"CalculateEmulsionCoveringSpace: EmulsionPerHole={emulsionPerHole}, EmulsionPerMeter={emulsionPerMeter}, Result={result}");
            return result;
        }

        public double CalculateEmulsionPerMeter(double diameter, double density)
        {
            double result = (22.0 / 7000.0) * Math.Pow(diameter / 2.0, 2) * density;
            MessageBox.Show($"CalculateEmulsionPerMeter: Diameter={diameter}, Density={density}, Result={result}");
            return result;
        }

        public double CalculateRemainingSpace(double averageDepth, double stemming, double emulsionCoveringSpace)
        {
            double result = averageDepth - (stemming + emulsionCoveringSpace);
            MessageBox.Show($"CalculateRemainingSpace: AverageDepth={averageDepth}, Stemming={stemming}, EmulsionCoveringSpace={emulsionCoveringSpace}, Result={result}");
            return result;
        }

        public double CalculateTotalAnfo(double anfoCoveringSpace, int numberOfHoles)
        {
            return anfoCoveringSpace * numberOfHoles;
        }

        public int CalculateTotalCount()
        {
            return DepthEntries.Sum(entry => entry.Depths.Count(depth => depth != 0));
        }

        public double CalculateTotalDepth()
        {
            return DepthEntries.Sum(entry => entry.Depths.Where(depth => depth != 0).Sum());
        }

        public double CalculateTotalEmulsion(double emulsionPerHole, int numberOfHoles)
        {
            return emulsionPerHole * numberOfHoles;
        }

        public double CalculateTotalVolume(double averageDepth, double spacing, double burden)
        {
            double result = averageDepth * _totalHoles * spacing * burden;
            MessageBox.Show($"average depth {averageDepth} , total holes {_totalHoles} ,spacing {spacing} , burden {burden}");
            return result;
        }

        private void CreateDepthGrid(int numberOfRows, int numberOfColumns)
        {
            DepthEntries = new ObservableCollection<DepthEntry>();

            for (int i = 0; i < numberOfRows; i++)
            {
                var entry = new DepthEntry(numberOfColumns);
                entry.PropertyChanged += DepthEntry_PropertyChanged;
                DepthEntries.Add(entry);
            }

            DepthsDataGrid.ItemsSource = DepthEntries;

            for (int i = 0; i < numberOfColumns; i++)
            {
                var depthColumn = new DataGridTextColumn
                {
                    Header = $"Depth {i + 1}",
                    Binding = new System.Windows.Data.Binding($"Depths[{i}]"),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };

                DepthsDataGrid.Columns.Add(depthColumn);
            }

            DepthsDataGrid.PreviewKeyDown += DepthsDataGrid_PreviewKeyDown;
        }

        private void DepthEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DepthEntry.Depths))
            {
                UpdateAverageDepth();
            }
        }

        private void DepthsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var uiElement = e.OriginalSource as UIElement;

                if (uiElement != null)
                {
                    e.Handled = true;
                    uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
        }

        public double CalculateTotalEmulsion65mm(double emulsion65mmPerHole, int numberOfHoles)
        {
            return ((emulsion65mmPerHole * numberOfHoles) * 1.5 / 16) * 25;
        }

        public double CalculateTotalEmulsion50mm(double emulsion50mmPerHole, int numberOfHoles)
        {
            return emulsion50mmPerHole * numberOfHoles;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            double averageDepth = CalculateAverageDepth();

            if (double.TryParse(DiameterTextBox.Text, out double diameter) &&
                double.TryParse(StemmingTextBox.Text, out double stemming) &&
                double.TryParse(EmulsionPerHoleTextBox.Text, out double emulsionPerHole) &&
                double.TryParse(Emulsion65mmPerHoleTextBox.Text, out double emulsion65mmPerHole) &&
                double.TryParse(Emulsion50mmPerHoleTextBox.Text, out double emulsion50mmPerHole) &&
                double.TryParse(EmulsionDensityTextBox.Text, out double emulsionDensity) &&
                double.TryParse(AnfoDensityTextBox.Text, out double anfoDensity) &&
                double.TryParse(SpacingTextBox.Text, out double spacing) &&
                double.TryParse(BurdenTextBox.Text, out double burden))
            {
                // Calculate Emulsion per meter
                double emulsionPerMeter = CalculateEmulsionPerMeter(diameter, emulsionDensity);
                Debug.WriteLine($"Emulsion per Meter: {emulsionPerMeter}");

                // Calculate Emulsion Covering Space
                double emulsionCoveringSpace = CalculateEmulsionCoveringSpace(emulsionPerHole, emulsionPerMeter);
                Debug.WriteLine($"Emulsion Covering Space: {emulsionCoveringSpace}");

                // Calculate Remaining Space
                double remainingSpace = CalculateRemainingSpace(averageDepth, stemming, emulsionCoveringSpace);
                Debug.WriteLine($"Remaining Space: {remainingSpace}");

                // Calculate ANFO per meter
                double anfoPerMeter = CalculateAnfoPerMeter(diameter, anfoDensity);
                Debug.WriteLine($"ANFO per Meter: {anfoPerMeter}");

                // Calculate ANFO Covering Space
                double anfoCoveringSpace = CalculateAnfoCoveringSpace(anfoPerMeter, remainingSpace);
                Debug.WriteLine($"ANFO Covering Space: {anfoCoveringSpace}");

                // Calculate Total ANFO
                double totalAnfo = CalculateTotalAnfo(anfoCoveringSpace, _totalHoles);
                Debug.WriteLine($"Total ANFO: {totalAnfo}");

                // Calculate Total Emulsion for 65 mm
                double totalEmulsion65mm = CalculateTotalEmulsion65mm(emulsion65mmPerHole, _totalHoles);
                Debug.WriteLine($"Total Emulsion 65 mm: {totalEmulsion65mm}");

                // Calculate Total Emulsion for 50 mm
                double totalEmulsion50mm = CalculateTotalEmulsion50mm(emulsion50mmPerHole, _totalHoles);
                Debug.WriteLine($"Total Emulsion 50 mm: {totalEmulsion50mm}");

                // Sum Total Emulsion
                double totalEmulsion = totalEmulsion65mm + totalEmulsion50mm;
                Debug.WriteLine($"Total Emulsion: {totalEmulsion}");

                // Display the results
                TotalEmulsionTextBlock.Text = $"Total Emulsion: {totalEmulsion:F2}";
                TotalAnfoTextBlock.Text = $"Total ANFO: {totalAnfo:F2}";
                TotalEmulsion65mmTextBlock.Text = $"Total Emulsion 65 mm: {totalEmulsion65mm:F2}";
                TotalEmulsion50mmTextBlock.Text = $"Total Emulsion 50 mm: {totalEmulsion50mm:F2}";
                double totalVolume = CalculateTotalVolume(averageDepth, spacing, burden);
                TotalVolumeTextBlock.Text = $"Total Volume: {totalVolume:F2}";

                UpdateAverageDepth();

                var report = new ReportB
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
                    EmulsionPerHole = emulsionPerHole,
                    EmulsionDensity = emulsionDensity,
                    AnfoDensity = anfoDensity,
                    Spacing = spacing,
                    Burden = burden
                };

                SharedData.Instance.Reports.Add(report);

                // Show the ReportWindow with the report data
                var reportWindow = new ReportWindow();
                reportWindow.Show();
                // Refresh the ListView to show updated reports

                // Close the current window
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid numbers for all fields.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateAverageDepth()
        {
            double averageDepth = CalculateAverageDepth();
            AverageDepthTextBlock.Text = $"Average Depth: {averageDepth:F2}";
            double totalDepth = CalculateTotalDepth();
            TotalDrillMeterTextBlock.Text = $"Total Depth: {totalDepth:F2}";

            int totalFilledHoles = CalculateTotalCount();
            TotalFilledHolesTextBlock.Text = $"Total Filled Holes: {totalFilledHoles}";
        }
    }
}