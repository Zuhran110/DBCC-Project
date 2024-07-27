using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
            Logins = new List<Login>
            {
                new Login { Id = 1, UserName = "admin", Password = "admin123", Role = "admin" },
                new Login { Id = 2, UserName = "GeneralManager", Password = "GM123", Role = "GeneralManager" },
                new Login { Id = 3, UserName = "BlastEngineer", Password = "BE123", Role = "BlastEngineer" },
                new Login { Id = 4, UserName = "StoreKeeper", Password = "SK123", Role = "StoreKeeper" }
            };
        }

        [RelayCommand]
        public void LogIn()
        {
            var user = Logins.FirstOrDefault(l => l.UserName == Username && l.Password == Password);

            if (user != null)
            {
                UserService.Instance.SetCurrentUser(user); // Set the current user in the UserService

                Window nextWindow = user.Role switch
                {
                    "admin" => new AdminWindow(),
                    "GeneralManager" => new GeneralManagerWindow(),
                    "BlastEngineer" => new BlastEngineerWindow(),
                    "StoreKeeper" => new StoreKeeperWindow(),
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