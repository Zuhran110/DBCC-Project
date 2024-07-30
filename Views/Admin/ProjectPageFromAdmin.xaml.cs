using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using WpfApp6.Models;

namespace WpfApp6.Views.Admin
{
    public partial class ProjectPageFromAdmin : Page
    {
        public ObservableCollection<Project> Projects { get; set; }
        public ObservableCollection<Login> Engineers { get; set; }
        private Project selectedProject;

        public ProjectPageFromAdmin()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                Projects = new ObservableCollection<Project>(context.Projects
                    .Include(p => p.ProjectEngineers)
                    .ThenInclude(pe => pe.Engineer)
                    .ToList());

                Engineers = new ObservableCollection<Login>(context.Login
                    .Where(l => l.Role == Login.Roles.BlastEngineer)
                    .ToList());
            }

            ProjectsDataGrid.ItemsSource = Projects;
            BlastingEngineerComboBox.ItemsSource = Engineers;
        }

        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (BlastingEngineerComboBox.SelectedItem is Login selectedEngineer)
            {
                var newProject = new Project
                {
                    ProjectName = ProjectNameTextBox.Text,
                    ProjectEngineers = new List<ProjectEngineer>
                    {
                        new ProjectEngineer { EngineerId = selectedEngineer.Id }
                    }
                };

                using (var context = new AppDbContext())
                {
                    context.Projects.Add(newProject);
                    context.SaveChanges();
                }

                Projects.Add(newProject);

                // Clear the input fields
                ProjectNameTextBox.Clear();
                BlastingEngineerComboBox.SelectedIndex = -1;
            }
        }

        private void ProjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectsDataGrid.SelectedItem is Project project)
            {
                selectedProject = project;
                ProjectNameTextBox.Text = project.ProjectName;
                BlastingEngineerComboBox.SelectedItem = project.ProjectEngineers.FirstOrDefault()?.Engineer;
            }
            else
            {
                selectedProject = null;
                ProjectNameTextBox.Clear();
                BlastingEngineerComboBox.SelectedIndex = -1;
            }
        }

        private void UpdateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProject != null && BlastingEngineerComboBox.SelectedItem is Login selectedEngineer)
            {
                using (var context = new AppDbContext())
                {
                    var project = context.Projects.Include(p => p.ProjectEngineers).FirstOrDefault(p => p.Id == selectedProject.Id);
                    if (project != null)
                    {
                        project.ProjectName = ProjectNameTextBox.Text;
                        project.ProjectEngineers.Clear();
                        project.ProjectEngineers.Add(new ProjectEngineer { EngineerId = selectedEngineer.Id });

                        context.SaveChanges();
                    }
                }

                selectedProject.ProjectName = ProjectNameTextBox.Text;
                selectedProject.ProjectEngineers.Clear();
                selectedProject.ProjectEngineers.Add(new ProjectEngineer { EngineerId = selectedEngineer.Id, Engineer = selectedEngineer });

                // Refresh the DataGrid to show updated values
                ProjectsDataGrid.Items.Refresh();

                // Clear the input fields
                ProjectNameTextBox.Clear();
                BlastingEngineerComboBox.SelectedIndex = -1;
            }
        }
    }
}
