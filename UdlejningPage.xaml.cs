using System;
using System.Collections.Generic;
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
    /// Interaction logic for UdlejningPage.xaml
    /// </summary>
    public partial class UdlejningPage : Page
    {
        private static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=svendeproevedb";

        private static MenuPage menuPage = new MenuPage();

        List<string> allItems = new List<string>();

        public UdlejningPage()
        {
            InitializeComponent();
            RunGetQuery();
        }

        // Går tilbage til hovedmenuen
        private void NavigateToMenu(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(menuPage);
        }

        // Opdatere comboboxen
        private void UpdateComboBox(object sender, RoutedEventArgs e)
        {
            RunGetQuery();
        }

        // Udlejer en vare
        private void UdlejVare(object sender, RoutedEventArgs e)
        {
            if (itemCombo.SelectedItem == null || customerNameText.Text == "")
            {
                MessageBox.Show("Du skal vælge en vare og skrive et navn før du kan udleje en vare!");
                return;
            }

            string query =
                "INSERT INTO ordrer(item, customername) VALUES ('" + itemCombo.Text + "','" + customerNameText.Text + "');" +
                "INSERT INTO historik(item, customername) VALUES ('" + itemCombo.Text + "','" + customerNameText.Text + "');" +
                "UPDATE lager SET itemstatus = 'Udlejet' WHERE itemname = '" + itemCombo.Text + "';";

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
                    allItems.Remove(itemCombo.SelectedItem.ToString());
                    itemCombo.Items.Refresh();
                    itemCombo.SelectedItem = null;
                    customerNameText.Text = "";
                    MessageBox.Show("Vare blev udlejet!");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error: " + a.Message);
            }
        }

        // Henter alle varer fra DB der har statussen "På lager"
        private void RunGetQuery()
        {
            allItems.Clear();

            itemCombo.ItemsSource = allItems;

            itemCombo.SelectedItem = null;

            string query = "SELECT * FROM lager WHERE itemstatus='På lager'";

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
                        allItems.Add(myReader.GetString(1));
                    }
                }
                else
                {
                    MessageBox.Show("Query successfully executed");
                }
                itemCombo.Items.Refresh();
            }
            catch(Exception e)
            {
                MessageBox.Show("Query error: " + e.Message);
            }


        }
    }
}
