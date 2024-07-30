using System.Windows;
using WpfApp6.Models;

namespace WpfApp6.Views.Blast_Engineer.ContentDialogue;

/// <summary>
/// Interaction logic for ProjectStoreSelectionWindow.xaml
/// </summary>
public partial class ProjectStoreSelectionWindow : Window
{
    public Project SelectedProject
    {
        get; private set;
    }

    public Store SelectedStore
    {
        get; private set;
    }

    public ProjectStoreSelectionWindow()
    {
        InitializeComponent();

        // Populate the ComboBoxes with sample data
        ProjectComboBox.ItemsSource = new List<Project>
            {
                new Project { ProjectName = "Project A" },
                new Project { ProjectName = "Project B" },
                new Project { ProjectName = "Project C" }
            };
        StoreComboBox.ItemsSource = new List<Store>
            {
                new Store { StoreName = "Store X" },
                new Store { StoreName = "Store Y" },
                new Store { StoreName = "Store Z" }
            };
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedProject = ProjectComboBox.SelectedItem as Project;
        SelectedStore = StoreComboBox.SelectedItem as Store;
        DialogResult = true;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}