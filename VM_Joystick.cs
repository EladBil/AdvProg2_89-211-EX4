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
            set
            {
                model.Throttle_1 = (float) value;
            }
        }
        public double VM_Throttle2
        {
            get { return model.Throttle_2; }
            set
            {
                model.Throttle_2 = (float) value;
            }
        }
        public double VM_Rudder
        {
            get { return model.Rudder; }
            set
            {
                model.Rudder = (float) value;
            }

        }
        public double VM_Aileron
        {
            get { return model.Aileron; }
            set
            {
                model.Aileron = (float) value;
            }
        }
        public double VM_Elevator
        {
            get { return model.Elevator; }
            set
            {
                model.Elevator = (float) value;
            }
        }
    }
}
