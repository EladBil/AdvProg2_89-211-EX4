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
using DrawingDLL;


namespace FlightSimADVProg2_ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM_Start VMStart;

        public MainWindow()
        {
            InitializeComponent();
            VMStart = new VM_Start();
            VMStart.VM_APIFileName = "playback_small.xml";
            VMStart.VM_CSVFileName = "reg_flight.csv";
            GraphsView.GivenGraphsViewModel = this.VMStart.VM_Graphs;
            VMStart.UserViewProperty = AnomalyDetectionView;
            GroundRelativeView.GivenGroundRelativeViewModel = this.VMStart.VM_GroundRelativeView;
            JoystickView.GivenJoystickVM = this.VMStart.VMJoystickProperty;
            PlaybackControlView.GivenPlaybackViewModel = this.VMStart.VMPlaybackControlsProperty;
            VMStart.Initialize();
            VMStart.StartAnimation();
        }

        private void GroundRelativeView_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void GraphsView_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
