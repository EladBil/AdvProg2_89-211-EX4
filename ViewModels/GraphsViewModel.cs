/*
 * mission 6, 7, 8.
 * Person, list of stuff, lin_reg
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FlightSimADVProg2_ex1.Model;

namespace FlightSimADVProg2_ex1.ViewModels
{
    class GraphsViewModel : INotifyPropertyChanged, IViewModel
    {
        public const string DEFAULT_CORRELATIVE_NAME = "No Correlated Feature";
        public const string NO_CORRELATIVE = "-1";
        public const string FRAME_INDEX_PROPERTY_NAME = "VM_IndexFrame";
        public const string CHOSEN_ATTRIBUTE_PROPERTY_NAME = "VM_ChosenAttributeName";
        public const string CORRELATIVE_ATTRIBUTE_PROPERTY_NAME = "VM_CorrelativeAttributeName";
        public const string CHOSEN_ATTRIBUTE_VLAUES_PROPERTY_NAME = "VM_ChoseAttributeValues";
        public const string CORRELATIVE_ATTRIBUTE_VLAUES_PROPERTY_NAME = "VM_CorrelativeAttributeValues";

        // Chosen feature
        private string chosen;
        // Most correlated feature
        private string cor;
        // Colomn of chosen feature
        private double[] chosenVal;
        // Colomn of corelated feature
        private double[] corVal;
        private int NumOfFrames;
        private List<string> ListAttributesNames;

        private IModel model;

        public GraphsViewModel(IModel model) 
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            NumOfFrames = 0; // We didn't Initialize yet! (bcs we dont have the info for that yet.)
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //returns the frame we are currently looking at
        public int VM_IndexFrame
        {
            get { return model.IndexFrame; }
            set
            {
                model.IndexFrame = value;
                NotifyPropertyChanged(FRAME_INDEX_PROPERTY_NAME);
            }
        }
        public string VM_ChosenAttributeName
        {
            get { return this.chosen; }
            set
            {
                //update the chosen feature
                chosen = value;
                //update the most correlated feature
                VM_CorrelativeAttributeName = model.GetMostCor(chosen);
                //update the row of the chosen feature
                VM_ChosenAttributeValues = GetValuesOfAttribute(chosen);
                //update the row of the most correlated feature
                if (!VM_CorrelativeAttributeName.Equals(DEFAULT_CORRELATIVE_NAME))
                {
                    VM_CorrelativeAttributeValues = CorChange(model.GetValuesSpecificAttribute(VM_CorrelativeAttributeName));
                } else
                {
                    VM_CorrelativeAttributeValues = ZeroFillArray();
                }
                NotifyPropertyChanged(CHOSEN_ATTRIBUTE_PROPERTY_NAME);
            }
        }
        public string VM_CorrelativeAttributeName
        {
            get { return cor; }
            set
            {
                cor = value;
                if (cor.Equals(NO_CORRELATIVE))
                {
                    cor = DEFAULT_CORRELATIVE_NAME;
                }
                NotifyPropertyChanged(CORRELATIVE_ATTRIBUTE_PROPERTY_NAME);
            }
        }
        public double[] VM_ChosenAttributeValues
        {
            get { return chosenVal; }
            set
            {
                chosenVal = value;
                NotifyPropertyChanged(CHOSEN_ATTRIBUTE_VLAUES_PROPERTY_NAME);
            }

        }

        public double[] VM_CorrelativeAttributeValues
        {
            get { return corVal; }
            set
            {
                corVal = value;
                NotifyPropertyChanged(CORRELATIVE_ATTRIBUTE_VLAUES_PROPERTY_NAME);
            }
        }


        //returns list of attributes
        public List<string> VM_AttributesNames
        {
            get { return this.ListAttributesNames; }
        }


        //returns colomn of attribute
        public double[] GetValuesOfAttribute(string attribute)
        {
            List<float> list = model.GetValuesSpecificAttribute(attribute);
            return FromListToArray(list);
        }

        //returns the number of rows in the csv
        public int GetOfNumberRows()
        {
            return this.NumOfFrames;
        }

        private void InitNumOfFrames()
        {
            this.NumOfFrames = model.GetNumberRows();
        }
        //returns array of 0's
        private double[] ZeroFillArray()
        {

            int j = this.NumOfFrames;
            double[] array = new double[j];
            for (int i = 0; i < j; i++)
            {
                array[i] = 0;
            }
            return array;
        }

        //returns an array created from list
        private double[] FromListToArray(List<float> list)
        {
            double[] array = new double[GetOfNumberRows()];
            int i = 0;
            foreach (float value in list)
            {
                array[i] = (double)value;
                i++;
            }
            return array;
        }
        //retruns the array of most correlated feature
        private double[] CorChange(List<float> list)
        {
            //if there isnt a correlated feature return an empty array
            if (VM_CorrelativeAttributeName.Equals(DEFAULT_CORRELATIVE_NAME))
            {
                return ZeroFillArray();
            }
            return FromListToArray(list);
        }

        public void Initialize()
        {
            InitNumOfFrames();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }

}
