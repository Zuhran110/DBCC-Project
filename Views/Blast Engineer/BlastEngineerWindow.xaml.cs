using System.Windows;

namespace WpfApp6.Views.Blast_Engineer;

/// <summary>
/// Interaction logic for BlastEngineerWindow.xaml
/// </summary>
public partial class BlastEngineerWindow : Window
{
    public BlastEngineerWindow()
    {
        InitializeComponent();
    }

    private void Dashboard_Btn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void Request_Btn_Click(object sender, RoutedEventArgs e)
    {
        MainFrameFromBE.Navigate(new ReportFromBE());
    }
}