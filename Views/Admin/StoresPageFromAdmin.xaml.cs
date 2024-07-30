using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Models;

namespace WpfApp6.Views.Admin
{
    /// <summary>
    /// Interaction logic for StoresPageFromAdmin.xaml
    /// </summary>
    public partial class StoresPageFromAdmin : Page
    {
        public ObservableCollection<Store> Stores { get; set; }

        public StoresPageFromAdmin()
        {
            InitializeComponent();
            Stores = new ObservableCollection<Store>
            {
                new Store { ID = 1, StoreName = "Main Store", Location = "Downtown", StoreKeeper = "Alice Johnson" },
                new Store { ID = 2, StoreName = "Branch Store", Location = "Uptown", StoreKeeper = "Bob Smith" }
            };
            StoresDataGrid.ItemsSource = Stores;
        }

        private void AddStoreButton_Click(object sender, RoutedEventArgs e)
        {
            var newStore = new Store
            {
                ID = Stores.Count + 1,
                StoreName = StoreNameTextBox.Text,
                Location = LocationTextBox.Text,
                StoreKeeper = StoreKeeperTextBox.Text
            };

            Stores.Add(newStore);

            // Clear the input fields
            StoreNameTextBox.Clear();
            LocationTextBox.Clear();
            StoreKeeperTextBox.Clear();
        }

        private void StoresDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StoresDataGrid.SelectedItem is Store selectedStore)
            {
                DetailStoreNameTextBox.Text = selectedStore.StoreName;
                DetailLocationTextBox.Text = selectedStore.Location;
                DetailStoreKeeperTextBox.Text = selectedStore.StoreKeeper;
            }
        }

        private void UpdateStoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (StoresDataGrid.SelectedItem is Store selectedStore)
            {
                selectedStore.Location = DetailLocationTextBox.Text;
                selectedStore.StoreKeeper = DetailStoreKeeperTextBox.Text;

                // Refresh the DataGrid to show updated values
                StoresDataGrid.Items.Refresh();
            }
        }
    }
}
