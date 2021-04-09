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

using FlightSimADVProg2_ex1.Model;
using FlightSimADVProg2_ex1.ViewModels;

namespace FlightSimADVProg2_ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyModel UnifedModel;
        VM_Start VMStart;

        public MainWindow()
        {
            InitializeComponent();
            UnifedModel = new MyModel(new MyTelnetFlightGearClientTCP());
        }

        private void GroundRelativeView_Loaded(object sender, RoutedEventArgs e)
        {
            GroundRelativeView.VMMODEL = UnifedModel;
        }

        private void GraphsView_Loaded(object sender, RoutedEventArgs e)
        {
            GraphsView.VMMODEL = UnifedModel;
        }
    }
}
