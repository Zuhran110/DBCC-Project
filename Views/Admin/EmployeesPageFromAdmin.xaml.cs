using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp6.Views.Admin;

/// <summary>
/// Interaction logic for EmployeesPageFromAdmin.xaml
/// </summary>
public partial class EmployeesPageFromAdmin : Page
{
    public ObservableCollection<Employee> Employees
    {
        get; set;
    }

    public EmployeesPageFromAdmin()
    {
        InitializeComponent();
        Employees = new ObservableCollection<Employee>
            {
                new Employee { ID = 1, Name = "John Doe", Position = "Manager", Department = "Sales" },
                new Employee { ID = 2, Name = "Jane Smith", Position = "Developer", Department = "IT" }
            };
        EmployeesDataGrid.ItemsSource = Employees;
    }

    private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
    {
        var newEmployee = new Employee
        {
            ID = Employees.Count + 1,
            Name = NameTextBox.Text,
            Position = PositionTextBox.Text,
            Department = DepartmentTextBox.Text
        };

        Employees.Add(newEmployee);

        // Clear the input fields
        NameTextBox.Clear();
        PositionTextBox.Clear();
        DepartmentTextBox.Clear();
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

    public string Position
    {
        get; set;
    }

    public string Department
    {
        get; set;
    }
}