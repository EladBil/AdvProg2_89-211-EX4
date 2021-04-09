using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
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
        private bool onlyonce = true;

        public GroundRelativeView()
        {
            InitializeComponent();
        }


        private MyModel mm;
        public MyModel VMMODEL
        {
            get { return mm; }
            set 
            {
                mm = value;
                if (onlyonce)
                {
                    vm5 = new VM_Mission5(mm);
                    DataContext = vm5;
                    onlyonce = false;
                }
            }
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
