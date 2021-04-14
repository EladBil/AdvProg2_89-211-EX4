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
using System.ComponentModel;

using FlightSimADVProg2_ex1.ViewModels;

namespace FlightSimADVProg2_ex1.SubViews
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>

    public partial class Joystick : UserControl
    {
        private const string AILERON_ATTRIBUTE_VMNAME = "VM_aileron";
        private const string ELEVATOR_ATTRIBUTE_VMNAME = "VM_elevator";

        private double StartingKnobPositionX;
        private double StartingKnobPositionY;
        private double XUnitsMove;
        private double YUnitsMove;
        private bool Onlyonce;
        private VM_Joystick jsvm;   // aka Joystick View Model

        public Joystick()
        {
            InitializeComponent();
            Onlyonce = true;
        }


        // A property for us to be able to get our View Model.
        public VM_Joystick GivenJoystickVM
        {
            get { return jsvm; }
            set 
            { 
                if (this.Onlyonce)
                {
                    jsvm = value;
                    InitSequence();
                }
            }
        }


        private void InitSequence()
        {
            Onlyonce = false;
            DataContext = jsvm;

            this.jsvm.PropertyChanged +=
               delegate (object sender, PropertyChangedEventArgs e)
               {
                   PropertyChangedManage(e.PropertyName);
               };
        }


        int ELEVATOR_POSPONER = 1;
        int AILERON_POSPONER = 1;
        private void PropertyChangedManage(string Name)
        {
            if (Name.Equals(AILERON_ATTRIBUTE_VMNAME))
            {
                if (AILERON_POSPONER % 16 == 0)
                {
                    ChangeAileronAttribute();
                }
                AILERON_POSPONER++;
            }
            if (Name.Equals(ELEVATOR_ATTRIBUTE_VMNAME))
            {
                if (ELEVATOR_POSPONER % 8 == 0)
                {
                    ChangeElevatorAttribute();
                }
                ELEVATOR_POSPONER++;
            }
        }

        
        private void ChangeAileronAttribute()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {//this refer to form in WPF application 
                knobPosition.X = jsvm.VM_Aileron * 32;
            }));
        }


        private void ChangeElevatorAttribute()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {//this refer to form in WPF application 
                knobPosition.Y = jsvm.VM_Elevator * 32;
            }));
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            
        }
    }
}
