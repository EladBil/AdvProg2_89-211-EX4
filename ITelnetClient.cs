using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using System.Net;

namespace FlightSimADVProg2_ex1.Model
{
    /// <summary>
    /// An interface whose function is to wrap the entire connection with an external server
    /// Performs basic server communication operations
    /// </summary>
    interface ITelnetClient
    {
        int Connect(string ip, int port);
        void Write(string command);
        string Read(); 
        void Disconnect();
        /// <summary>
        /// Returns whether the connection is correct
        /// </summary>
        /// <returns>bool</returns>
        bool isConnected();

    }

    /// <summary>
    /// Our implementation aims to create a shell between the model and the socket and connect and send the data
    /// </summary>
    class MyTelnetFlightGearClientTCP : ITelnetClient
    {
        TcpClient client;
        NetworkStream stream;
        byte[] bytes = new byte[4098];


        /// <summary>
        /// Establishes a connection with the server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns>
        /// 0 success
        /// -1 failed
        /// </returns>
        public int Connect(string ip, int port)
        {
            // Connect to a Remote server  
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            try
            {
                //TcpClient client = new TcpClient(ip, port);
                this.client = new TcpClient(ip, port);
                this.stream = this.client.GetStream();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// write string to server
        /// </summary>
        /// <param name="command"> string to send</param>
        public void Write(string command)
        {

            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
                this.stream.Write(data, 0, data.Length);
               


            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
        /// <summary>
        /// Irrelevant reading in our project
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            try
            {
                // Receive the response from the remote device.    
                int bytesRec = this.stream.Read(this.bytes);
                //  Console.WriteLine("Echoed test = {0}",   Encoding.ASCII.GetString(this.bytes, 0, bytesRec));

                return Encoding.ASCII.GetString(this.bytes, 0, bytesRec);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
            return "-1";
        }
        /// <summary>
        ///disconnect form server
        /// </summary>
        public void Disconnect()
        {
            try
            {
                this.client.Close();
                this.stream.Close();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
        /// <summary>
        /// Check if there is a connection by checking the stream and socket
        /// </summary>
        /// <returns></returns>
        public bool isConnected()
        {
            if (this.client == null || this.stream == null)
            {
                return false;
            }
            return true;
        }

    }


    /// <summary>
    /// The class was written as an option for udp communication but is not used in our project
    /// </summary>
    class MyTelnetFlightGearClientUDP : ITelnetClient
    {



        private int port;
        UdpClient receiver;

        public int Connect(string ip, int port)
        {

            this.port = port;

            this.receiver = new UdpClient();
            // this.receiver.Connect(IPAddress.Parse(ip) , this.port);
            this.receiver.Connect(ip, this.port);
            return 0;
        }
        public void Write(string command)
        {
            //   Console.WriteLine("Socket connected to {0}", this.sender.RemoteEndPoint.ToString());

            // Encode the data string into a byte array.
            try
            {
                Byte[] senddata = Encoding.ASCII.GetBytes(command);
                this.receiver.Send(senddata, senddata.Length);

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
        public string Read()
        {
            return "-1";
        }
        public void Disconnect()
        {

            this.receiver.Client.Shutdown(SocketShutdown.Receive);
            this.receiver.Client.Close();
        }

        public bool isConnected()
        {
           
            return false;
        }

    }
}