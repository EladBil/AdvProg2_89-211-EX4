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

        private bool onlyonce = true;

        public GroundRelativeView()
        {
            InitializeComponent();
        }


        private VM_Mission5 grvm;
        public VM_Mission5 GivenGroundRelativeViewModel
        {
            get { return grvm; }
            set 
            {
                grvm = value;
                if (onlyonce)
                {
                    onlyonce = false;
                    InitSequence();
                }
            }
        }

        private void InitSequence()
        {
            DataContext = grvm;
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
