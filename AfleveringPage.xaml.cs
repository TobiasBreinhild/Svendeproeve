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
    /// Interaction logic for AfleveringPage.xaml
    /// </summary>
    public partial class AfleveringPage : Page
    {
        private static MenuPage menuPage = new MenuPage();

        private static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=svendeproevedb";

        List<Ordre> allItems = new List<Ordre>();

        public AfleveringPage()
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

        // Aflevere en udlejet vare
        private void ReturnItem(object sender, RoutedEventArgs e)
        {
            if(OrderCombo.SelectedItem == null)
            {
                MessageBox.Show("Du skal vælge en vare først!");
                return;
            }

            string query = "DELETE FROM ordrer WHERE item = '" + OrderCombo.SelectedItem.ToString() + "';" +
                           "UPDATE lager SET itemstatus = 'På lager' WHERE itemname = '" + OrderCombo.SelectedItem.ToString() + "';";

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
                    string tempName = OrderCombo.SelectedItem.ToString();
                    OrderCombo.SelectedItem = null;
                    OrderCombo.Items.Remove(tempName);
                    OrderCombo.Items.Refresh();
                    MessageBox.Show("Vare afleveret!");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error: " + a.Message);
            }
        }

        // Henter alle ordrer fra DB
        private void RunGetQuery()
        {
            allItems.Clear();
            OrderCombo.Items.Clear();

            string query = "SELECT * FROM ordrer;";

            MySqlConnection databaseconnection = new MySqlConnection(MySQLConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseconnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseconnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                if (myReader.HasRows)
                {
                    int i = 0;

                    while (myReader.Read())
                    {
                        allItems.Add(new Ordre(Int32.Parse(myReader.GetString(0)), myReader.GetString(2), myReader.GetString(1)));

                        OrderCombo.Items.Add(allItems[i].Itemname);

                        i++;
                    }
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