using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using System;

using FlightSimADVProg2_ex1.Model;
using FlightSimADVProg2_ex1.ViewModels;

namespace FlightSimADVProg2_ex1.SubViews
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl
    {
        GraphsViewModel vm;
        const string MAINGRAPHNAME = "Value/Frame";
        const string CORRELATIVEPROPERTYNAME = "VM_CorVal";
        const string INDEXFRAMEPROPERTY = "VM_indexFrame";
        int NumOfFrames;
        double[] ArrayOfFrameNumbers;
        double[] DefaultArray;
        bool Onlyonce = true;


        public GraphsView()
        {
            InitializeComponent();
        }

        private void InitVMGraphs()
        {
            vm.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    PropertyChangedManage(e.PropertyName);
                };
        }


        private MyModel mm;
        public MyModel VMMODEL
        {
            get { return mm; }
            set
            {
                mm = value;
                if (Onlyonce)
                {
                    vm = new GraphsViewModel(mm);
                    LoadMyModelSequence();
                }
            }
        }


        private void LoadMyModelSequence()
        {
            DataContext = vm;
            Onlyonce = false;
            // Init the view Model;
            InitVMGraphs();
        }


        private void StartSequence()
        {
            // Inits the 2 graphs: Chosen Parameter Graph; Most Coorelative to Chosen Parameter Graph.
            InitMainGraphs();
        }

        // Initialize the main graphs.
        private void InitMainGraphs()
        {
            DefaultArray = new double[NumOfFrames];
            ArrayOfFrameNumbers = new double[NumOfFrames];
            // Zero fill for default graph.
            for (int i = 0; i < NumOfFrames; i++)
            {
                DefaultArray[i] = 0;
                ArrayOfFrameNumbers[i] = i;
            }
            ParamAndCoorelativeGraph.plt.PlotSignal(DefaultArray, maxRenderIndex: 0, label: "Default");
            ParamAndCoorelativeGraph.plt.PlotSignal(DefaultArray, maxRenderIndex: 0, label: "No Correaltive Found");
            InitGraphSettings();
        }

        private void PropertyChangedManage(string name)
        {
            if (name.Equals(CORRELATIVEPROPERTYNAME))
            {
                UpdateGraphValues();
                return;
            }
        }

        private void UpdateGraphValues()
        {
            ParamAndCoorelativeGraph.plt.Clear();

            /*int MinRender = 0;
            if (vm.VM_IndexFrame > 30)
            {
                MinRender = vm.VM_IndexFrame - 30;
            }
            if (vm.VM_ChosenVal == null)
            {
                ParamAndCoorelativeGraph.plt.PlotSignal(DefaultArray, minRenderIndex: MinRender,
                maxRenderIndex: vm.VM_IndexFrame, label: vm.VM_Chosen);
            } else
            {
                ParamAndCoorelativeGraph.plt.PlotSignal(vm.VM_ChosenVal, minRenderIndex: MinRender,
                maxRenderIndex: vm.VM_IndexFrame, label: vm.VM_Chosen);
            }
            if (vm.VM_CorVal == null)
            {
                ParamAndCoorelativeGraph.plt.PlotSignal(DefaultArray, minRenderIndex: MinRender,
                    maxRenderIndex: vm.VM_IndexFrame, label: vm.VM_Cor);
            } else
            {
                ParamAndCoorelativeGraph.plt.PlotSignal(vm.VM_CorVal, minRenderIndex: MinRender,
                maxRenderIndex: vm.VM_IndexFrame, label: vm.VM_Cor);
            }*/
            int MinRender = 0;
            if (vm.VM_IndexFrame > 30)
            {
                MinRender = vm.VM_IndexFrame - 30;
            }
            ParamAndCoorelativeGraph.plt.PlotSignal(vm.VM_ChosenAttributeValues, minRenderIndex: MinRender,
                maxRenderIndex: vm.VM_IndexFrame, label: vm.VM_ChosenAttributeName);
            ParamAndCoorelativeGraph.plt.PlotSignal(vm.VM_CorrelativeAttributeValues, minRenderIndex: MinRender,
                maxRenderIndex: vm.VM_IndexFrame, label: vm.VM_CorrelativeAttributeName);
            try {
                ParamAndCoorelativeGraph.Render();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void InitGraphSettings()
        {
            ParamAndCoorelativeGraph.plt.Legend();
            ParamAndCoorelativeGraph.plt.Style(ScottPlot.Style.Blue3);
            ParamAndCoorelativeGraph.plt.AxisAuto();
        }

        private void FlightParameterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string muchwow = (sender as ListBox).SelectedItem.ToString();
            vm.VM_ChosenAttributeName = muchwow;
        }

        private void ParamAndCoorelativeGraph_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
