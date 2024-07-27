using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Models;
using WpfApp6.Views.Blast_Engineer.ContentDialogue;

namespace WpfApp6.Views.Blast_Engineer
{
    public partial class ReportWindow : Window, INotifyPropertyChanged
    {
        public List<ReportB> Reports
        {
            get; set;
        }

        public double TotalSumEmulsion => Reports.Sum(r => r.TotalEmulsion);
        public double TotalSumEmulsion65mm => Reports.Sum(r => r.TotalEmulsion65mm);
        public double TotalSumEmulsion50mm => Reports.Sum(r => r.TotalEmulsion50mm);
        public double TotalSumAnfo => Reports.Sum(r => r.TotalAnfo);
        public double TotalSumVolume => Reports.Sum(r => r.TotalVolume);

        public event PropertyChangedEventHandler PropertyChanged;

        public ReportWindow()
        {
            InitializeComponent();
            Reports = SharedData.Instance.Reports;
            DataContext = this;
            PopulateListView();
        }

        public void PopulateListView()
        {
            ReportList.ItemsSource = null;
            ReportList.ItemsSource = Reports;
            OnPropertyChanged(nameof(TotalSumEmulsion));
            OnPropertyChanged(nameof(TotalSumEmulsion65mm));
            OnPropertyChanged(nameof(TotalSumEmulsion50mm));
            OnPropertyChanged(nameof(TotalSumAnfo));
            OnPropertyChanged(nameof(TotalSumVolume));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            // Implement navigation to dashboard
        }

        private void AddCalculation_Click(object sender, RoutedEventArgs e)
        {
            AddRequestWindow addRequestWindow = new AddRequestWindow();
            bool? result = addRequestWindow.ShowDialog();

            if (result == true)
            {
                int numberOfColumns = addRequestWindow.NumberOfColumns;
                int totalHoles = addRequestWindow.NumberOfHoles;

                EnterDepthsWindow enterDepthsWindow = new EnterDepthsWindow(totalHoles, numberOfColumns);
                bool? depthsResult = enterDepthsWindow.ShowDialog();

                if (depthsResult == true)
                {
                    // Retrieve and calculate values (similar to previous example)
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
                    PopulateListView();
                }
            }
            else
            {
                MessageBox.Show("Request Cancelled", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MaterialComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                var parentStackPanel = comboBox.Parent as StackPanel;
                if (parentStackPanel != null)
                {
                    var textBox = parentStackPanel.Children.OfType<TextBox>().FirstOrDefault();
                    if (textBox != null)
                    {
                        textBox.Visibility = Visibility.Visible;
                    }
                }

                AddNewComboBoxAndTextBox();
            }
        }

        private void AddNewComboBoxAndTextBox()
        {
            var newStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 10)
            };

            var newComboBox = new ComboBox
            {
                Width = 200,
                Margin = new Thickness(10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            newComboBox.SelectionChanged += MaterialComboBox_SelectionChanged;

            var items = new List<string>
    {
        "ANFO", "EMULSION 40mm", "EMULSION 50mm", "EMULSION 65mm",
        "Detonating Cord 10gm/m", "Detonating Cord 40gm/m", "BOOSTERS 250 grm",
        "NON-ELECTRIC DETONATORS 6m", "NON-ELECTRIC DETONATORS 10m",
        "NON-ELECTRIC DETONATORS 12m", "NON-ELECTRIC DETONATORS 15m",
        "DETONATING RELAY 17ms", "DETONATING RELAY 25ms",
        "DETONATING RELAY 42ms", "DETONATING RELAY 67ms",
        "NON-ELECTRIC CONNECTORS 17ms 4m", "NON-ELECTRIC CONNECTORS 25ms 4m",
        "NON-ELECTRIC CONNECTORS 42ms 4m", "NON-ELECTRIC CONNECTORS 67ms 4m",
        "ELECTRIC DETONATORS (SDD) 4m"
    };

            foreach (var item in items)
            {
                newComboBox.Items.Add(new ComboBoxItem { Content = item });
            }

            newStackPanel.Children.Add(newComboBox);

            var newTextBox = new TextBox
            {
                Width = 100,
                Margin = new Thickness(10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Visibility = Visibility.Collapsed
            };

            newStackPanel.Children.Add(newTextBox);

            MainStackPanel.Children.Add(newStackPanel);
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var newReport = new ReportB
            {
                Date = DateTime.Now
            };

            foreach (var child in MainStackPanel.Children)
            {
                if (child is StackPanel stackPanel)
                {
                    var comboBox = stackPanel.Children.OfType<ComboBox>().FirstOrDefault();
                    var textBox = stackPanel.Children.OfType<TextBox>().FirstOrDefault();

                    if (comboBox != null && textBox != null && comboBox.SelectedItem != null)
                    {
                        if (double.TryParse(textBox.Text, out double quantity))
                        {
                            newReport.Materials.Add((comboBox.SelectedItem.ToString(), quantity));
                        }
                    }
                }
            }

            // Save to shared instance or any other desired storage
            SharedData.Instance.Reports.Add(newReport);

            // Calculate totals
            double totalAnfo = SharedData.Instance.Reports.Sum(r => r.TotalAnfo);
            double totalEmulsion = SharedData.Instance.Reports.Sum(r => r.TotalEmulsion);
            double totalVolume = SharedData.Instance.Reports.Sum(r => r.TotalVolume);

            // Set totals in the new report
            newReport.TotalAnfo = totalAnfo;
            newReport.TotalEmulsion = totalEmulsion;
            newReport.TotalVolume = totalVolume;

            // Open the ReportDetailsWindow
            var reportDetailsWindow = new ReportDetails
            {
                DataContext = newReport
            };
            reportDetailsWindow.ShowDialog();
        }
    }
}