/*
 * View Model Scroll bar + speed control
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FlightSimADVProg2_ex1.Model;

namespace FlightSimADVProg2_ex1.ViewModels
{
    class VM_ScrollBar : INotifyPropertyChanged
    {
        private IModel model;
        //speed we are viewing the flight
        private float playbackSpeed;
        //the frame we are looking at (for the scroll bar)
        private int indexFrame;
        public VM_ScrollBar(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        //getter and setter for index frame
        public int VM_IndexFrame
        {
            get { return model.IndexFrame; }
            set
            {
                indexFrame = value;
                model.IndexFrame = indexFrame;
            }
        }
        //get's and seta speed we are playing the flight
        public float VM_PlaybackSpeed
        {
            get { return playbackSpeed; }
            set
            {
                playbackSpeed = value;
                model.SetRefreshRate(playbackSpeed);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //returns how many rows there are in csv. Good for knowing how big the scroll bar should be
        public int VM_GetNumberRows
        {
            get { return model.GetNumberRows(); }
        }
    }
}
