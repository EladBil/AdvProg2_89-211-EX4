using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using System.Net;

namespace model
{
    interface ITelnetClient
    {
        void Connect(string ip, int port);
        void Write(string command);
        string Read(); // blocking call
        void Disconnect();

    }


    class MyTelnetFlightGear
    {
        byte[] bytes = new byte[1024];
        Socket sender;
        
        public void Connect(string ip, int port) {

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.    
            this.sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            this.sender.Connect(remoteEP);

        }
        public void Write(string command) {
            Console.WriteLine("Socket connected to {0}",
                       this.sender.RemoteEndPoint.ToString());

            // Encode the data string into a byte array.    
            byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

            // Send the data through the socket.    
            int bytesSent = this.sender.Send(msg);
        }
        public string Read() {
            // Receive the response from the remote device.    
            int bytesRec = this.sender.Receive(this.bytes);
            Console.WriteLine("Echoed test = {0}",
                Encoding.ASCII.GetString(this.bytes, 0, bytesRec));

            return ""; }
        public void Disconnect() {
            this.sender.Shutdown(SocketShutdown.Both);
            this.sender.Close();

        }




        /*





    byte[] bytes = new byte[4096];


        private Socket sender;
        public void Connect(string ip, int port)
        {

           


            try
            {
                // Connect to a Remote server  
                // Get Host IP Address that is used to establish a connection  
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
                // If a host has multiple addresses, you will get a list of addresses  
               
                
                
                // IPAddress ipAddress = host.AddressList[0];
               // IPAddress ipAddress = IPAddress.Parse(ip);

                //IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);




                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);


                Socket sender2 = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);


                // Create a TCP/IP  socket.    
                this.sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

               





                try
                {
                    // Connect to Remote EndPoint  
                    this.sender.Connect(remoteEP);
                    // Encode the data string into a byte array.    
                    byte[] msg = Encoding.ASCII.GetBytes("Yedidya Bachar");

                    // Send the data through the socket.    
                    int bytesSent = sender.Send(msg);
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
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void Write(string command)
        {
            try
            {
                // Encode the data string into a byte array.    
                byte[] msg = Encoding.ASCII.GetBytes(command);

                // Send the data through the socket.    
                int bytesSent = sender.Send(msg);
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
            int bytesRec;
            try
            {
                // Receive the response from the remote device.    
                bytesRec = this.sender.Receive(bytes);
                return Encoding.ASCII.GetString(bytes, 0, bytesRec);
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
            return "dont read";





           

        }
        public void Disconnect()
        {
            try
            {
                // Release the socket.    
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

    }*/
    }
}
