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

namespace FlightSimADVProg2_ex1.SubViews
{
    /// <summary>
    /// Interaction logic for PlaybackView.xaml
    /// </summary>
    public partial class PlaybackView : UserControl
    {
        public PlaybackView()
        {
            InitializeComponent();
        }


        private PlaybackControlsViewModel pcvm;
        public PlaybackControlsViewModel GivenPlaybackViewModel
        {
            get { return this.pcvm; }
            set
            {
                this.pcvm = value;
                InitializeByModel();
            }
        }


        private void InitializeByModel()
        {
            DataContext = this.pcvm;
            UpdateLayout();
        }


        public void PlayButtonClicked(object sender, RoutedEventArgs e)
        {
            this.pcvm.PlayFlight();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            this.pcvm.Pause();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temp = (sender as ComboBox).SelectedItem.ToString();
            this.GivenPlaybackViewModel.VM_FlightRate = float.Parse(temp);
        }

        private void IndexFrameSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.GivenPlaybackViewModel.VM_CurrentFrame = (int) e.NewValue;
        }
    }
}