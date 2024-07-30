using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Models;

namespace WpfApp6.Views.Admin
{
    public partial class EmployeesPageFromAdmin : Page
    {
        public ObservableCollection<Login> Employees
        {
            get; set;
        }

        public EmployeesPageFromAdmin()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            using (var context = new AppDbContext())
            {
                var employees = context.Login.ToList();
                Employees = new ObservableCollection<Login>(employees);
            }
            EmployeesDataGrid.ItemsSource = Employees;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRole = ((ComboBoxItem)RoleComboBox.SelectedItem)?.Content.ToString();

            var newEmployee = new Login
            {
                UserName = NameTextBox.Text,
                Department = DepartmentTextBox.Text,
                Password = PasswordBox.Password,
                Role = selectedRole
            };

            using (var context = new AppDbContext())
            {
                context.Login.Add(newEmployee);
                context.SaveChanges();
            }

            Employees.Add(newEmployee);

            // Clear the input fields
            NameTextBox.Clear();
            DepartmentTextBox.Clear();
            PasswordBox.Clear();
            RoleComboBox.SelectedIndex = -1;
        }
    }
}
