# AdvProg2_89-211-EX4

Flight Data Analysis 2021, 
version 1.0, 
Authors: Elad Bilman, Shmuel Yaish. Yedidya Bachar

Note: This program runs on an x86 platform. Please make sure your device is compatable with x86 framework before running.

Announcement: We would like to mention that one of our very own creators, Shmuel Yaish, created
a video explaining how to create a C++ dll to use in C# that was viewed 
and used by most of the class and has helped so far over 600 people! 
You can watch the turorial at this link https://www.youtube.com/watch?v=3efOjwKb9p4&t=910s

Video explaining our project:
Part 1: https://youtu.be/Xk6Z57QrCIU
Part 2: https://youtu.be/libUHjocNaA

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

4. The following folder must be extracted    "FlightSimADVProg2_ex1.zip"

Go to the directory netcoreapp3.1 in   "FlightSimADVProg2_ex1\FlightSimADVProg2_ex1\bin\x86\Debug\netcoreapp3.1"
You can insert the dll into this folder and additional files

5. Instructions for loading dll
The following dll files must be placed in the folder of the app's executable. 
5.1. HadDLL.dll C++ dynamic library which includes all the functions in the file "libraries.h" 
5.2. DrawingDLL.dll C# dynamic which includes the following: 
5.2.1. A function: CallDraw(double[] x, double[] y, int[] z, int w) 
5.2.1. The xaml class of the DLL must be called "UserControl1"

7.When you have finished preparing, click on the file  "FlightSimADVProg2_ex1.exe"


After the app has uploaded
8.Enter the path of the xml file in the appropriate place 

9.Enter the path of flight data file that you want to explore in the appropriate place 

10.Enter the path of flight data file that you want the anomaly detection system to learn to make a comparison in the appropriate place 

11.Press start

12.The app will begin to run and display the Flight Gear and all 

About the design:

The program is based on the MVVM design pattern. There is the Start window and
Main Window which are in the main directory.
The other folders work as follows:

Subviews:

These are displays including, but not limited to, the Joystick which displays the aileron 
and the elevator. The PlaybackView, which displays a scroll bar to select a frame to jump to,
pause button, play button, and a video playback speed controler! There is also GraphsView
which displays a selected detail of the flight along with it's most correlated feature,
and calls the DLL to display the anomalys of those features. And much more!

View Models:

These are VM's to connect the veiw with the model. There are multiple VM's so that different
parts of the program are independent of other parts, creating modularity in the program.
In addition, there is VM_Start which is also used to initiate the other View Models and 
to get the program started.

Model:

The model is based on the interface IModel. For easy use and read the model is divided up
into partial classes such as ModelFunctionBasics, the constructor and the getters and 
setters of the flight details, and ModelFunctionAPI with the calculation algorithms.
There is also ITelnetClient which is an interface of a socket which connects you to the flight
gear. In addition there is anomalAlgorithms which wraps the information coming in from the
C++ dll so the model doesn't have to deal with the dll.

Plug-ins:

Our program works with C++ and C# dlls. The C++ dll is used by the model to calculate
the anomalies and deal with the information coming in from the flight CSV file. In
addition we have the C# dll which displays the anomalies on the screen based off the type
of the anomaly. Our program does not know what type of anomly we are testing looking for,
therefor it has outsourced it's anomaly calculations to an external dll so that you can
check and display any type of anomaly you'd like!

