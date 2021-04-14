# AdvProg2_89-211-EX4

Flight Data Analysis 2021 version 1.0 Authors: Elad Bilman Shmuel Yaish Yedidya Bachar

Note: This program runs on an x86 platform. Please make sure your device is compatable with x86 framework before running.

Operating Instructions:

1. Download and Install the flight gear app from https://www.flightgear.org/

2. The xml settings file should be placed in the folder where you installed flightgear and inside to 0/data/protocol 
Example windows: C:\Program Files\FlightGear 2020.3.6\data\Protocol

3. In Flight Gear settings inside the Additional Setting box, enter the following lines:
--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small 
--fdm=null

Click the fly button

Explanation: playback_small - The name of the settings file without it's extension You may change these fields as you please

Recommendation: After running the application, the port of the flight gear may be caught and actions must be taken to release him.

4. The folder must be extracted    "FlightSimADVProg2_ex1.zip"

Enter to folder netcoreapp3.1 in   "FlightSimADVProg2_ex1\FlightSimADVProg2_ex1\bin\x86\Debug\netcoreapp3.1"
You can insert the dll into this folder
and additional files
When you have finished preparing, click on the file  "FlightSimADVProg2_ex1.exe"

5. Instructions for loading dll
The following dll files must be placed in the folder of the app's executable. 
5.1. HadDLL.dll C++ dynamic library which includes all the functions in the file "libraries.h" 
5.2. DrawingDLL.dll C# dynamic which includes the following: 
5.2.1. A function: CallDraw(double[] x, double[] y, int[] z, int w) 
5.2.1. The xaml class of the DLL must be called "UserControl1"

After the app has uploaded
6.Enter the path of the xml file in the appropriate place 

7.Enter the path of flight data file that you want to explore in the appropriate place 

8.Enter the path of flight data file that you want the anomaly detection system to learn to make a comparison in the appropriate place 

9.Press start

10.The app will begin to run and display the Flight Gear and all 
