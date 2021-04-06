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

namespace FlightSimADVProg2_ex1.SubViews
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl
    {
        public GraphsView()
        {
            InitializeComponent();
        }

        private void FlightParameterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
       
        
        private void CoorelativeParamGraph_Loaded(object sender, RoutedEventArgs e)
        {
            //double[] ChosenParam = ViewModel.VM_MostCorrelatedParameter; 
        }
        
        private void CoorelationGraph_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
