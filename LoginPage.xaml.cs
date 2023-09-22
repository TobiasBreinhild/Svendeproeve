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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=svendeproevedb";

        private static MenuPage menuPage = new MenuPage();

        public LoginPage()
        {
            InitializeComponent();
        }

        // Tjekker efter om der er skrevet noget i tekstboksene eller ej
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (usernameText.Text == "" || passwordText.Password == "")
            {
                MessageBox.Show("Du skal skrive noget ind i begge felter!");
            }
            else
            {
                LoginQuery();
            }
        }

        // Henter brugerene ned får at kunne logge ind (IKKE GOD SIKKERHED, I KNOW)
        private void LoginQuery()
        {
            string query = "SELECT * FROM brugere";

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
                        if(usernameText.Text == myReader.GetString(1) && passwordText.Password == myReader.GetString(2))
                        {
                            usernameText.Text = "";
                            passwordText.Password = "";

                            NavigationService.Navigate(menuPage);

                            return;
                        }
                    }

                    MessageBox.Show("Brugernavn eller kodeord passer ikke!\nPrøv igen!");
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
