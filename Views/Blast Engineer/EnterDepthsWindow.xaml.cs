using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp6.Views.Blast_Engineer;

/// <summary>
/// Interaction logic for EnterDepthsWindow.xaml
/// </summary>
public partial class EnterDepthsWindow : Window
{
    public ObservableCollection<DepthEntry> DepthEntries
    {
        get; private set;
    }

    public EnterDepthsWindow(int numberOfRows, int numberOfColumns)
    {
        InitializeComponent();
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

    private void UpdateAverageDepth()
    {
        double totalDepth = DepthEntries.Sum(entry => entry.Depths.Sum());
        int totalCount = DepthEntries.Sum(entry => entry.Depths.Count);

        double averageDepth = totalCount > 0 ? totalDepth / totalCount : 0;

        AverageDepthTextBlock.Text = $"Average Depth: {averageDepth:F2}";
    }

    private void Submit_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
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