using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimADVProg2_ex1
{
    /// <summary>
    /// Interaction logic for StartingScreen.xaml
    /// </summary>
    public partial class StartingScreen : Window
    {
        private const string XML_EXTENSION = ".xml";
        private const string CSV_EXTENSION = ".csv";

        private MainWindow MainScreen;
        public StartingScreen()
        {
            InitializeComponent();
            MainScreen = new MainWindow();
        }

        private void LetsStart_Clicked(object sender, RoutedEventArgs e)
        {
            this.MainScreen.StartMainWindow();
            this.MainScreen.Show();
            this.Close();
        }

        private void OPENCSVBUTTON_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.DefaultExt = CSV_EXTENSION;

            opd.ShowDialog();

            this.MainScreen.MainWindowCSVPathProperty = opd.FileName;
        }

        private void OpenPropertXml_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.DefaultExt = XML_EXTENSION;

            opd.ShowDialog();

            this.MainScreen.MainWindowAPIPathProperty = opd.FileName;
        }

        private void OpenAnomalyCSV_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.DefaultExt = CSV_EXTENSION;

            opd.ShowDialog();

            this.MainScreen.MainWindowAnomalPathProperty = opd.FileName;
        }
    }
}
