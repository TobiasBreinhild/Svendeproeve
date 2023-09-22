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
    /// Interaction logic for OrdrerPage.xaml
    /// </summary>
    public partial class OrdrerPage : Page
    {
        private static MenuPage menuPage = new MenuPage();

        private static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=svendeproevedb";

        ObservableCollection<Ordre> allOrders = new ObservableCollection<Ordre>();

        public OrdrerPage()
        {
            InitializeComponent();

            RunGetQuery();
        }

        // Går tilbage til hovedmenuen
        private void NavigateToMenu(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(menuPage);
        }

        private void OpenHistory(object sender, RoutedEventArgs e)
        {
            ViewHistoryBorder.Visibility = Visibility.Visible;

            GetHistory();
        }

        private void CloseHistory(object sender, RoutedEventArgs e)
        {
            ViewHistoryBorder.Visibility = Visibility.Collapsed;
        }

        // Opdatere griddet
        private void UpdateGrid(object sender, RoutedEventArgs e)
        {
            allOrders.Clear();

            RunGetQuery();

            ordersDatagrid.Items.Refresh();
        }

        // Henter alle ordrer fra DB
        private void RunGetQuery()
        {
            ordersDatagrid.ItemsSource = allOrders;

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
                    while (myReader.Read())
                    {
                        allOrders.Add(new Ordre(Int32.Parse(myReader.GetString(0)), myReader.GetString(2).ToString(), myReader.GetString(1).ToString()));
                    }

                    ordersDatagrid.Items.Refresh();
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

        private void GetHistory()
        {
            ordersHistoryDatagrid.Items.Clear();

            string query = "SELECT * FROM historik;";

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
                        ordersHistoryDatagrid.Items.Add(new Ordre(Int32.Parse(myReader.GetString(0)), myReader.GetString(1).ToString(), myReader.GetString(2).ToString()));
                    }

                    ordersHistoryDatagrid.Items.Refresh();
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
