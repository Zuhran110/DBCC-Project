using System.Windows;

namespace WpfApp6.Views.Blast_Engineer.ContentDialogue;

/// <summary>
/// Interaction logic for AddRequestWindow.xaml
/// </summary>
public partial class AddRequestWindow : Window
{
    public string NumberOfHoles
    {
        get; private set;
    }

    public AddRequestWindow()
    {
        InitializeComponent();
    }

    private void Submit_Click(object sender, RoutedEventArgs e)
    {
        NumberOfHoles = NumberOfHolesTextBox.Text;
        this.DialogResult = true; // Set dialog result to true to indicate successful completion
        this.Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false; // Set dialog result to false to indicate cancellation
        this.Close();
    }
}