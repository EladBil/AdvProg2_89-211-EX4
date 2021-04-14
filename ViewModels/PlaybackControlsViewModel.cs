using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using FlightSimADVProg2_ex1.Model;

namespace FlightSimADVProg2_ex1.ViewModels
{
    public class PlaybackControlsViewModel : IViewModel
    {
        public const string ATTRIBUTE_PREFIX = "VM_";

        private IModel Model;
        public PlaybackControlsViewModel(IModel Model)
        {
            this.Model = Model;
            this.Model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(ATTRIBUTE_PREFIX + e.PropertyName);
            };
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        private List<float> VMSpeedList;
        public List<float> VM_SpeedList
        {
            get { return this.VMSpeedList = this.Model.GetListOfspeeds(); }
            set { }
        }


        private int MaxFrame;
        public int VM_MaxFrame
        {
            get { return this.MaxFrame = this.Model.GetNumberRows(); }
            set { }
        }


        private int CurrentFrame;
        public int VM_CurrentFrame
        {
            get { return this.CurrentFrame = this.Model.IndexFrame; }
            set 
            {
                this.CurrentFrame = value;
                this.Model.IndexFrame = this.CurrentFrame;
            }
        }


        private float FlightRate;
        public float VM_FlightRate
        {
            get { return this.FlightRate = this.Model.GetRefreshRate(); }
            set 
            {
                this.FlightRate = value;
                this.Model.SetRefreshRate(this.FlightRate);
            }
        }

        // This will envoke all the functions of the model to start the flight.
        public void PlayFlight()
        {
            this.Model.start();
        }

        public void Pause()
        {
            this.Model.pause();
        }


        public void Initialize()
        {
            this.Model.Connect("127.0.0.1", 5403);
            this.MaxFrame = this.Model.GetNumberRows();
        }


        public void StartAnimation()
        {
            // Nothing extra needed to be added for now.
        }
    }
}
