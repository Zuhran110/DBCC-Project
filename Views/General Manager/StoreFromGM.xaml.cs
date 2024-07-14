using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp6.Views.General_Manager;

/// <summary>
/// Interaction logic for StoreFromGM.xaml
/// </summary>
public partial class StoreFromGM : Page
{
    public ObservableCollection<Store> Stores
    {
        get; set;
    }

    public StoreFromGM()
    {
        InitializeComponent();
        Stores = new ObservableCollection<Store>
            {
                new Store { ID = 1, StoreName = "Main Store", Location = "Downtown", StoreKeeper = "Alice Johnson" },
                new Store { ID = 2, StoreName = "Branch Store", Location = "Uptown", StoreKeeper = "Bob Smith" }
            };
        StoresDataGrid.ItemsSource = Stores;
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

    public class Store
    {
        public int ID
        {
            get; set;
        }

        public string StoreName
        {
            get; set;
        }

        public string Location
        {
            get; set;
        }

        public string StoreKeeper
        {
            get; set;
        }
    }
}