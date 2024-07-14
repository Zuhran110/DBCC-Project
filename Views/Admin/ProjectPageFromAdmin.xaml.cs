using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp6.Views.Admin;

/// <summary>
/// Interaction logic for ProjectPageFromAdmin.xaml
/// </summary>
public partial class ProjectPageFromAdmin : Page
{
    public ObservableCollection<Project> Projects
    {
        get; set;
    }

    public ProjectPageFromAdmin()
    {
        InitializeComponent();
        Projects = new ObservableCollection<Project>
            {
                new Project { ID = 1, ProjectName = "Project Alpha", BlastingEngineer = "John Doe" },
                new Project { ID = 2, ProjectName = "Project Beta", BlastingEngineer = "Jane Smith" }
            };
        ProjectsDataGrid.ItemsSource = Projects;
    }

    private void AddProjectButton_Click(object sender, RoutedEventArgs e)
    {
        var newProject = new Project
        {
            ID = Projects.Count + 1,
            ProjectName = ProjectNameTextBox.Text,
            BlastingEngineer = BlastingEngineerTextBox.Text
        };

        Projects.Add(newProject);

        // Clear the input fields
        ProjectNameTextBox.Clear();
        BlastingEngineerTextBox.Clear();
    }

    private void ProjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProjectsDataGrid.SelectedItem is Project selectedProject)
        {
            DetailProjectNameTextBox.Text = selectedProject.ProjectName;
            DetailBlastingEngineerTextBox.Text = selectedProject.BlastingEngineer;
        }
    }

    private void UpdateProjectButton_Click(object sender, RoutedEventArgs e)
    {
        if (ProjectsDataGrid.SelectedItem is Project selectedProject)
        {
            selectedProject.BlastingEngineer = DetailBlastingEngineerTextBox.Text;

            // Refresh the DataGrid to show updated values
            ProjectsDataGrid.Items.Refresh();
        }
    }

    public class Project
    {
        public int ID
        {
            get; set;
        }

        public string ProjectName
        {
            get; set;
        }

        public string BlastingEngineer
        {
            get; set;
        }
    }
}