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

namespace Medisave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestDatabaseConnection();
        }

        private void btnAddNewPatient_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow navWindow = new NavigationWindow
            {
                Width = 800,  // Set preferred width
                Height = 600, // Set preferred height
                ResizeMode = ResizeMode.CanMinimize // Prevent excessive resizing
            };
            navWindow.Navigate(new pgNewPatientForm());
            navWindow.Show();

        }
        private void TestDatabaseConnection()
        {
            var dbContext = new DatabaseHelper();
            if (dbContext.TestConnection())
            {
                MessageBox.Show("Successfully connected to database!", "Connection Test");
            }
            else
            {
                MessageBox.Show("Failed to connect to database. Please check your connection settings.", "Connection Error");
            }
        }
    }
}
