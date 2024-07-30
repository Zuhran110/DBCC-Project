using System.Windows;
using SourceChord.FluentWPF;
using WpfApp6.Services;
using WpfApp6.ViewModels;

namespace WpfApp6.Views.Admin
{
    public partial class AdminWindow : AcrylicWindow
    {
        private readonly UserViewModel _userViewModel;
        public AdminWindow()
        {
            InitializeComponent();
            var currentUser = UserService.Instance.CurrentUser;
            _userViewModel = new UserViewModel();
            DataContext = _userViewModel;
        }

        private void Empoyees_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new EmployeesPageFromAdmin());
        }

        private void Store_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new StoresPageFromAdmin());
        }

        private void Project_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new ProjectPageFromAdmin());
        }

        private void Logout_Btn_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window
            this.Close();

            // Show the login window or perform logout actions
            Application.Current.Shutdown(); // To completely close the application

            // Alternatively, if you want to show the login screen again:
            // var loginWindow = new MainWindow(); // Assuming MainWindow is your login screen
            // loginWindow.Show();
        }
    }
}
