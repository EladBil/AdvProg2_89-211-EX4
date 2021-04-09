/*
 * VM for loading API and flight CSV at start of the program
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FlightSimADVProg2_ex1.Model;

namespace FlightSimADVProg2_ex1.ViewModels
{

    class VM_Start : INotifyPropertyChanged, IViewModel
    {
        private IModel model;
        
        // A constructor to initialize all the View Models.
        public VM_Start()
        {
            this.model = new MyModel(new MyTelnetFlightGearClientTCP());
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            this.VMGroundRelativeView = new VM_Mission5(this.model);
            this.VMGraphs = new GraphsViewModel(this.model);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        // A property of the View Model of user story 5.
        private VM_Mission5 VMGroundRelativeView;
        public VM_Mission5 VM_GroundRelativeView
        {
            get { return this.VMGroundRelativeView; }
        }

        private GraphsViewModel VMGraphs;
        public GraphsViewModel VM_Graphs
        {
            get { return this.VMGraphs; }
        }


        private string APIFileName;
        public string VM_APIFileName
        {
            get { return APIFileName; }
            set { APIFileName = value; }
        }

        private string CSVFileName;
        public string VM_CSVFileName
        {
            get { return CSVFileName; }
            set { CSVFileName = value; }
        }

        //load the API upon start of program
        private void LoadAPI(string fileAPI)
        {
            model.LoadingAPI(fileAPI);
        }
        //load the CSV upon start of program
        private void LoadCSV(string fileCSV)
        {
            model.LoadingCSV(fileCSV);
        }

        // This should only initialize the parametrs but not start the all reading sequence!!!\
        public void Initialize()
        {
            // After calling LoadAPI we have the list of Attributes of the flight.
            LoadAPI(APIFileName);
            // After calling LoadCSV we have the all the values of the attributes of the flight.
            // And we have the number of Frames.
            LoadCSV(CSVFileName);
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
