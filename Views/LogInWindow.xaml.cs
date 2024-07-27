using System.Windows;
using SourceChord.FluentWPF;
using WpfApp6.ViewModels;

namespace WpfApp6.Views
{
    public partial class LogInWindow : AcrylicWindow
    {
        public LogInWindow()
        {
            InitializeComponent();
            DataContext = new LogInWindowViewModel();
        }

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;
            PasswordTextBox.Text = PasswordBox.Password;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordBox.Password = PasswordTextBox.Text;
        }

        private void LogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (LogInWindowViewModel)DataContext;
            viewModel.Password = PasswordBox.Password;
            viewModel.LogIn();
        }
    }
}