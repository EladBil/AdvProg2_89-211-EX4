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
using System.Windows.Navigation;
using System.Windows.Shapes;

using FlightSimADVProg2_ex1.ViewModels;
using FlightSimADVProg2_ex1.Model;

namespace FlightSimADVProg2_ex1.SubViews
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GroundRelativeView : UserControl
    {
        VM_Mission5 vm5;
        VM_Start vmstart;

        public GroundRelativeView()
        {
            InitializeComponent();
            MyModel mm = new MyModel(new MyTelnetFlightGearClientUDP());
            vmstart = new VM_Start(mm);
            vmstart.VM_LetsStart("reg_flight.csv", "playback_small.xml");
            vm5 = new VM_Mission5(mm);
           
            DataContext = vm5;
        }

        private void VelocityValue_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
        private void DegreeValue_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HeightValue_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }


        private void ProgressBar_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ProgressBar_ValueChanged_2(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
