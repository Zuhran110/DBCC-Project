using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp6.Views.General_Manager;

/// <summary>
/// Interaction logic for ProjectFromGM.xaml
/// </summary>
public partial class ProjectFromGM : Page
{
    public ObservableCollection<Project> Projects
    {
        get; set;
    }

    public ProjectFromGM()
    {
        InitializeComponent();
        Projects = new ObservableCollection<Project>
            {
                new Project { ID = 1, ProjectName = "Project Alpha", BlastingEngineer = "John Doe" },
                new Project { ID = 2, ProjectName = "Project Beta", BlastingEngineer = "Jane Smith" }
            };
        ProjectsDataGrid.ItemsSource = Projects;
    }

    private void ProjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProjectsDataGrid.SelectedItem is Project selectedProject)
        {
            DetailProjectNameTextBox.Text = selectedProject.ProjectName;
            DetailBlastingEngineerTextBox.Text = selectedProject.BlastingEngineer;
        }
    }

    private void EditProjectButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        // Implement the logic to edit the selected project.
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