# AdvProg2_89-211-EX4

Flight Data Analysis 2021 
version 1.0
Authors:
Elad Bilman 
Shmuel Yaish
Yedidya Bachar

Note: This program runs on an x86 platform. Please make sure your
device is compatable with x86 framework before running.

Operating Instructions:
1. Download the flight gear app
2. The xml settings file should be placed in the folder where you installed flightgear
And inside to  0/data/protocol
Example windows: 
C:\Program Files\FlightGear 2020.3.6\data\Protocol
3. In Flight Gear settings inside the Additional Setting box,
enter the following lines:

--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
--fdm=null

Explanation:
playback_small - The name of the settings file without it's extension
5400 - Port number 
127.0.0.1 - ip
You may change these fields as you please

4.The following dll files must be placed in the folder of the app's executable.
4.1. HadDLL.dll C++ dynamic library which includes all the functions in
the file "libraries.h"
4.2. DrawingDLL.dll C# dynamic which includes the following:
4.2.1. A function: CallDraw(double[] x, double[] y, int[] z, int w)
4.2.1. The xaml class of the DLL must be called "UserControl1"

5.Launch the app
6.Enter the port and ip assigned to the Flight Gear in the appropriate place
7.Enter the path of the xml file in the appropriate place
8.Enter the path of the csv file in the appropriate place
9.Press start
10.The app will begin to run and display the Flight Gear and all the features
