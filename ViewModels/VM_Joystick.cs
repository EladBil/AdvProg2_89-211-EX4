/*
 * VM Joystick
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FlightSimADVProg2_ex1.Model;

namespace FlightSimADVProg2_ex1.SubViews
{
    class VM_Joystick : INotifyPropertyChanged
    {
        private IModel model;
        public VM_Joystick(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
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
        //returns the throttle_1
        public double VM_Throttle1
        {
            get { return model.Throttle_1; }
        }
        public double VM_Throttle2
        {
            get { return model.Throttle_2; }
        }
        public double VM_Rudder
        {
            get { return model.Rudder; }
        }
        public double VM_Aileron
        {
            get { return model.Aileron; }
        }
        public double VM_Elevator
        {
            get { return model.Elevator; }
        }
    }
}
