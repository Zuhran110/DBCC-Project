using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SourceChord.FluentWPF;
using WpfApp6.Views.Admin;
using WpfApp6.Views.Blast_Engineer;
using WpfApp6.Views.General_Manager;
using WpfApp6.Views.Store_Keeper;

namespace WpfApp6.Views
{
    public partial class LogInWindow : AcrylicWindow
    {
        public ObservableCollection<Login> Logins
        {
            get; set;
        }

        public LogInWindow()
        {
            InitializeComponent();

            Logins = new ObservableCollection<Login>
            {
                new Login { Id = 1, UserName = "admin", Password = "admin123", Role = "admin" },
                new Login { Id = 2, UserName = "GeneralManager", Password = "GM123", Role = "GeneralManager" },
                new Login { Id = 3, UserName = "BlastEngineer", Password = "BE123", Role = "BlastEngineer" },
                new Login { Id = 4, UserName = "StoreKeeper", Password = "SK123", Role = "StoreKeeper" }
            };
        }

        private void LogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = ShowPasswordCheckBox.IsChecked == true ? PasswordTextBox.Text : PasswordBox.Password;

            var user = Logins.FirstOrDefault(l => l.UserName == username && l.Password == password);

            if (user != null)
            {
                Window nextWindow = null;
                switch (user.Role)
                {
                    case "admin":
                        nextWindow = new AdminWindow(); // Replace with your admin window
                        break;

                    case "GeneralManager":
                        nextWindow = new GeneralManagerWindow(); // Replace with your General Manager window
                        break;

                    case "BlastEngineer":
                        nextWindow = new BlastEngineerWindow(); // Replace with your Blast Engineer window
                        break;

                    case "StoreKeeper":
                        nextWindow = new StoreKeeperWindow(); // Replace with your Store Keeper window
                        break;
                }

                if (nextWindow != null)
                {
                    nextWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = PasswordBox.Password;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordTextBox.Text;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;
        }
    }

    public class Login
    {
        public int Id
        {
            get; set;
        }

        public string Role
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }
    }
}