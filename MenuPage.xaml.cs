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

namespace SvendeproeveProjekt
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private static UdlejningPage udlejningPage = new UdlejningPage();
        private static AfleveringPage afleveringPage = new AfleveringPage();
        private static LagerPage lagerPage = new LagerPage();
        private static OrdrerPage ordrerPage = new OrdrerPage();
        private static LoginPage loginPage = new LoginPage();

        public MenuPage()
        {
            InitializeComponent();
        }

        // Går til udlejnings siden
        private void NavigateToUdlejning(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(udlejningPage);
        }

        // Går til afleverings siden
        private void NavigateToAflevering(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(afleveringPage);
        }

        // Går til lager siden
        private void NavigateToLager(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(lagerPage);
        }

        // Går til ordrer siden
        private void NavigateToOrdrer(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ordrerPage);
        }

        // Går tilbage til login siden
        private void NavigateToLogin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(loginPage);
        }
    }
}
