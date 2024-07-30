using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp6.Models;
using WpfApp6.Services;
using WpfApp6.Views.Admin;
using WpfApp6.Views.Blast_Engineer;
using WpfApp6.Views.General_Manager;
using WpfApp6.Views.Store_Keeper;

namespace WpfApp6.ViewModels
{
    public partial class LogInWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool showPassword;

        public List<Login> Logins
        {
            get; set;
        }

        public LogInWindowViewModel()
        {
        }

        [RelayCommand]
        public void LogIn()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Username and password cannot be empty", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Login user;
            using (var context = new AppDbContext())
            {
                user = context.Login.FirstOrDefault(l => l.UserName == Username && l.Password == Password);  // Change Logins to Login
            }

            if (user != null)
            {
                UserService.Instance.SetCurrentUser(user); // Set the current user in the UserService

                Window nextWindow = user.Role switch
                {
                    Login.Roles.Admin => new AdminWindow(),
                    Login.Roles.GeneralManager => new GeneralManagerWindow(),
                    Login.Roles.BlastEngineer => new BlastEngineerWindow(),
                    Login.Roles.StoreKeeper => new StoreKeeperWindow(),
                    _ => null
                };

                if (nextWindow != null)
                {
                    nextWindow.Show();
                    Application.Current.MainWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
