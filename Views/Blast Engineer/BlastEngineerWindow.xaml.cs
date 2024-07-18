using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SourceChord.FluentWPF;

namespace WpfApp6.Views.Blast_Engineer;

/// <summary>
/// Interaction logic for BlastEngineerWindow.xaml
/// </summary>
public partial class BlastEngineerWindow : AcrylicWindow
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