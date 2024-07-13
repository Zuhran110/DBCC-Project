using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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