using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using System.Net;

namespace FlightSimADVProg2_ex1.Model
{
    interface ITelnetClient
    {
        int Connect(string ip, int port);
        void Write(string command);
        string Read(); // blocking call
        void Disconnect();

    }


    class MyTelnetFlightGearClientTCP : ITelnetClient
    {
        byte[] bytes = new byte[4098];
        Socket sender;

        public int Connect(string ip, int port)
        {
            // Connect to a Remote server  
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            try
            {



                IPHostEntry host = Dns.GetHostEntry(ip);
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP  socket.    
                this.sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    this.sender.Connect(remoteEP);
                    return 0;
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    return -1;
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    return -1;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    return -1;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }


        }
        public void Write(string command)
        {
            //   Console.WriteLine("Socket connected to {0}", this.sender.RemoteEndPoint.ToString());

            // Encode the data string into a byte array.
            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(command);

                // Send the data through the socket.    
                int bytesSent = this.sender.Send(msg);


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
            try
            {
                // Receive the response from the remote device.    
                int bytesRec = this.sender.Receive(this.bytes);
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
        public void Disconnect()
        {
            try
            {
                this.sender.Shutdown(SocketShutdown.Both);
                this.sender.Close();
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



    }



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



    }
}
