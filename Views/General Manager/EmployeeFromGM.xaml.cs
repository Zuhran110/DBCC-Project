using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp6.Views.General_Manager;

/// <summary>
/// Interaction logic for EmployeeFromGM.xaml
/// </summary>
public partial class EmployeeFromGM : Page
{
    public ObservableCollection<Employee> Employees
    {
        get; set;
    }

    public EmployeeFromGM()
    {
        InitializeComponent();
        Employees = new ObservableCollection<Employee>
            {
                new Employee { ID = 1, Name = "John Doe", Role = "Manager", Position = "Sales Manager", Department = "Sales" },
                new Employee { ID = 2, Name = "Jane Smith", Role = "Developer", Position = "Software Developer", Department = "IT" }
            };
        EmployeesDataGrid.ItemsSource = Employees;
    }

    private void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (EmployeesDataGrid.SelectedItem is Employee selectedEmployee)
        {
            DetailIDTextBox.Text = selectedEmployee.ID.ToString();
            DetailNameTextBox.Text = selectedEmployee.Name;
            DetailRoleTextBox.Text = selectedEmployee.Role;
            DetailPositionTextBox.Text = selectedEmployee.Position;
            DetailDepartmentTextBox.Text = selectedEmployee.Department;
        }
    }
}

public class Employee
{
    public int ID
    {
        get; set;
    }

    public string Name
    {
        get; set;
    }

    public string Role
    {
        get; set;
    }

    public string Position
    {
        get; set;
    }

    public string Department
    {
        get; set;
    }
}