using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using System;

using FlightSimADVProg2_ex1.Model;
using FlightSimADVProg2_ex1.ViewModels;
using DrawingDLL;

namespace FlightSimADVProg2_ex1.SubViews
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl
    {
        const string DEFAULT_DIDNT_CHOOSE = "Default: PleaseChoose!";
        const int DEFAULT_MAX_RENDER = 30;
        const int DEFAULT_MIN_RENDER = 0;

        int NumOfFrames;
        double[] ArrayOfFrameNumbers;
        double[] DefaultArray;
        bool Onlyonce = true;
        bool InitializedChosenParam;


        public GraphsView()
        {
            InitializeComponent();
            this.InitializedChosenParam = false;
        }


        // This is how we get the instance of the ViewModel.
        private GraphsViewModel gvm;
        public GraphsViewModel GivenGraphsViewModel
        {
            get { return gvm; }
            set
            {
                if (Onlyonce)
                {
                    this.gvm = value;
                    LoadMyModelSequence();
                }
            }
        }


        // When the right model is loaded we need to initialize it.
        private void LoadMyModelSequence()
        {
            Onlyonce = false;
            DataContext = this.gvm;
            this.gvm.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    PropertyChangedManage(e.PropertyName);
                };
        }



        // This will be called, when we get notified about a change of Property.
        private void PropertyChangedManage(string name)
        {
            if (name.Equals(GraphsViewModel.ATTRIBUTS_NAMES_PROPERTY_NAME))
            {
                InitDefaultGraphScreen();
                return;
            }
            // Update the graph if anither attribute was chosen.
            if (name.Equals(GraphsViewModel.CORRELATIVE_ATTRIBUTE_VLAUES_PROPERTY_NAME))
            {
                UpdateGraphValues();
                this.InitializedChosenParam = true;
                return;
            }
            if (name.Equals(GraphsViewModel.FRAME_INDEX_PROPERTY_NAME))
            {
                // Update the graph every 5 Frames.
                if (this.InitializedChosenParam && gvm.VM_IndexFrame % 5 == 0)
                {
                    UpdateGraphValues();
                }
            }
        }


        // This will init the first view of the graphs and will set their options.
        private void InitDefaultGraphScreen()
        {
            // Setting a default array of points for the graphs.
            this.NumOfFrames = this.gvm.GetOfNumberRows();
            this.DefaultArray = new double[this.NumOfFrames];
            this.ArrayOfFrameNumbers = new double[this.NumOfFrames];
            for (int i = 0; i < this.NumOfFrames; i++)
            {
                this.DefaultArray[i] = 0;
                this.ArrayOfFrameNumbers[i] = i;
            }
 
            ParamAndCorrelativeGraph.plt.PlotSignal(DefaultArray,
                minRenderIndex:DEFAULT_MIN_RENDER, maxRenderIndex:DEFAULT_MAX_RENDER, label: DEFAULT_DIDNT_CHOOSE);
            ParamAndCorrelativeGraph.plt.PlotSignal(DefaultArray,
                minRenderIndex:DEFAULT_MIN_RENDER, maxRenderIndex: DEFAULT_MAX_RENDER, label: DEFAULT_DIDNT_CHOOSE);

            // Setting the graphs style and view options.
            ParamAndCorrelativeGraph.plt.Legend();
            ParamAndCorrelativeGraph.plt.Style(ScottPlot.Style.Blue3);
            ParamAndCorrelativeGraph.plt.AxisAuto();
        }


        // This will update the graph visually! with our current parametrs.
        // This function will be called every 5 frames or every change of choice attribute.
        private void UpdateGraphValues()
        {
            ParamAndCorrelativeGraph.plt.Clear();
            int MinRender = 0;
            if (this.gvm.VM_IndexFrame > 30)
            {
                MinRender = this.gvm.VM_IndexFrame - 30;
            }
            ParamAndCorrelativeGraph.plt.PlotSignal(this.gvm.VM_ChosenAttributeValues, minRenderIndex: MinRender,
                maxRenderIndex: this.gvm.VM_IndexFrame, label: this.gvm.VM_ChosenAttributeName);
            ParamAndCorrelativeGraph.plt.PlotSignal(this.gvm.VM_CorrelativeAttributeValues, minRenderIndex: MinRender,
                maxRenderIndex: this.gvm.VM_IndexFrame, label: this.gvm.VM_CorrelativeAttributeName);
            
            this.Dispatcher.Invoke((Action)(() =>
            {
                ParamAndCorrelativeGraph.Render();
            }));
            if (this.gvm.UserViewSetProperty)
            {
                this.gvm.AnomalyViewProperty.CallDraw(
                    this.gvm.VM_ChosenAttributeValues, this.gvm.VM_CorrelativeAttributeValues, 
                    this.gvm.FramesAnomlayProperty, this.gvm.VM_IndexFrame);
            }
        }


        // This function will extract the Name of the attribute chosen and sends it to VM
        // to update the info about this Attribute and his Correlative.
        private void FlightParameterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string muchwow = (sender as ListBox).SelectedItem.ToString();
            this.gvm.VM_ChosenAttributeName = muchwow;
        }

        private void ParamAndCoorelativeGraph_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
