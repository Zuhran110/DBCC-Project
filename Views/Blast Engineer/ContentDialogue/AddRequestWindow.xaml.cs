using System.Windows;
using WpfApp6.Services;

namespace WpfApp6.Views.Blast_Engineer.ContentDialogue
{
    public partial class AddRequestWindow : Window
    {
        public int NumberOfHoles
        {
            get; private set;
        }

        public int NumberOfRows
        {
            get; private set;
        }

        public int NumberOfColumns
        {
            get; private set;
        }

        public AddRequestWindow()
        {
            InitializeComponent();
            var currentUser = UserService.Instance.CurrentUser;
            if (currentUser != null)
            {
                // Use currentUser info as needed
                UserNameTextBlock.Text = $"Logged in as: {currentUser.UserName}";
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NumberOfHolesTextBox.Text, out int numberOfHoles) &&
                int.TryParse(NumberOfColumnsTextBox.Text, out int numberOfColumns))
            {
                NumberOfHoles = numberOfHoles;
                NumberOfColumns = numberOfColumns;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid numbers for holes, rows, and columns.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}