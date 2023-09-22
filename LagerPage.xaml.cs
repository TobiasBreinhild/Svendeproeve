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
using MySql.Data.MySqlClient;

namespace SvendeproeveProjekt
{
    /// <summary>
    /// Interaction logic for LagerPage.xaml
    /// </summary>
    public partial class LagerPage : Page
    {
        private static MenuPage menuPage = new MenuPage();

        private static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=svendeproevedb";

        ObservableCollection<Vare> allItems = new ObservableCollection<Vare>();

        public LagerPage()
        {
            InitializeComponent();

            RunGetQuery();
        }

        // Går tilbage til hovedmenuen
        private void NavigateToMenu(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(menuPage);
        }

        // Åbner en popup hvor man kan tilføje en ny vare
        private void OpenAddNew(object sender, RoutedEventArgs e)
        {
            AddNewBorder.Visibility = Visibility.Visible;
        }

        private void OpenChangeStatus(object sender, RoutedEventArgs e)
        {
            ChangeStatusBorder.Visibility = Visibility.Visible;
        }

        // Lukker ned for popuppen
        private void StopAdding(object sender, RoutedEventArgs e)
        {
            NewItemText.Text = "";
            AddNewBorder.Visibility = Visibility.Collapsed;
        }

        private void StopChanging(object sender, RoutedEventArgs e)
        {
            itemStatusCombo.SelectedItem = null;
            itemNewStatusCombo.SelectedItem = null;
            ChangeStatusBorder.Visibility = Visibility.Collapsed;
        }

        // Tilføjer en ny vare til lageret
        private void AddNewItem(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO lager(itemname, itemstatus) VALUES ('" + NewItemText.Text + "','På lager');";

            MySqlConnection databaseconnection = new MySqlConnection(MySQLConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseconnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseconnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {

                    }
                }
                else
                {
                    NewItemText.Text = "";
                    AddNewBorder.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Ny vare tilføjet til lageret!");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error: " + a.Message);
            }
        }

        // Opdatere griddet
        private void UpdateGrid(object sender, RoutedEventArgs e)
        {
            allItems.Clear();

            RunGetQuery();

            itemDatagrid.Items.Refresh();
        }

        private void ChangeItemStatus(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE lager SET itemstatus = '" + itemNewStatusCombo.Text + "' WHERE itemname = '" + itemStatusCombo.Text + "';";

            MySqlConnection databaseconnection = new MySqlConnection(MySQLConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseconnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseconnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {

                    }
                }
                else
                {
                    itemStatusCombo.SelectedItem = null;
                    itemNewStatusCombo.SelectedItem = null;
                    ChangeStatusBorder.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Varen's status blev skiftet!");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error: " + a.Message);
            }
        }

        // Henter alle varer fra DB
        private void RunGetQuery()
        {
            itemStatusCombo.SelectedItem = null;
            itemStatusCombo.Items.Clear();

            itemDatagrid.ItemsSource = null;

            itemDatagrid.ItemsSource = allItems;

            string query = "SELECT * FROM lager;";

            MySqlConnection databaseconnection = new MySqlConnection(MySQLConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseconnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseconnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        allItems.Add(new Vare(myReader.GetString(1).ToString(), myReader.GetString(2).ToString()));

                        itemStatusCombo.Items.Add(myReader.GetString(1));
                    }

                    itemDatagrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Query successfully executed");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Query error: " + e.Message);
            }


        }
    }
}
