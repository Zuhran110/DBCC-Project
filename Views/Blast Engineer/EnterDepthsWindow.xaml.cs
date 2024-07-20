using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SourceChord.FluentWPF;

namespace WpfApp6.Views.Blast_Engineer
{
    public partial class EnterDepthsWindow : Window
    {
        public ObservableCollection<DepthEntry> DepthEntries
        {
            get; private set;
        }

        private int _totalHoles;

        public EnterDepthsWindow(int numberOfRows, int numberOfColumns, int totalHoles)
        {
            InitializeComponent();
            _totalHoles = totalHoles;
            TotalHolesTextBlock.Text = $"Total Holes: {_totalHoles}";
            CreateDepthGrid(numberOfRows, numberOfColumns);
            UpdateAverageDepth();
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

        private double CalculateTotalDepth()
        {
            return DepthEntries.Sum(entry => entry.Depths.Where(depth => depth != 0).Sum());
        }

        private int CalculateTotalCount()
        {
            return DepthEntries.Sum(entry => entry.Depths.Count(depth => depth != 0));
        }

        private double CalculateAverageDepth()
        {
            double totalDepth = CalculateTotalDepth();
            int totalCount = CalculateTotalCount();
            return totalCount > 0 ? totalDepth / totalCount : 0;
        }

        private void UpdateAverageDepth()
        {
            double averageDepth = CalculateAverageDepth();
            AverageDepthTextBlock.Text = $"Average Depth: {averageDepth:F2}";
        }

        private double CalculateTotalVolume(double averageDepth, double spacing, double burden)
        {
            return averageDepth * _totalHoles * spacing * burden;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            double averageDepth = CalculateAverageDepth();

            if (double.TryParse(SpacingTextBox.Text, out double spacing) &&
                double.TryParse(BurdenTextBox.Text, out double burden))
            {
                double totalVolume = CalculateTotalVolume(averageDepth, spacing, burden);
                TotalVolumeTextBlock.Text = $"Total Volume: {totalVolume:F2}";
            }
            else
            {
                MessageBox.Show("Please enter valid numbers for spacing and burden.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class DepthEntry : System.ComponentModel.INotifyPropertyChanged
    {
        public ObservableCollection<double> Depths
        {
            get; private set;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public DepthEntry(int numberOfColumns)
        {
            Depths = new ObservableCollection<double>(new double[numberOfColumns]);
            Depths.CollectionChanged += Depths_CollectionChanged;
        }

        private void Depths_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Depths));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}