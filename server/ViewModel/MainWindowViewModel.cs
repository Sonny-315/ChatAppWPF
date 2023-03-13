using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using server.ViewModel;
using System.Windows;
using server.ViewModel.Commands;



namespace server.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        TcpListener listener;
        TcpClient client;
        Thread thr;

        public RelayCommand SendCommand { get; set; }
        public RelayCommand ConnectCommand { get; set; }

        public MainWindowViewModel()
        {
            SendCommand = new RelayCommand(Send);
            ConnectCommand = new RelayCommand(Connect);
        }

        private void Send(object obj)
        {
            if (client == null)
            {
                MessageBox.Show("Error");
                return;
            }
            try
            {
                NetworkStream stream = client.GetStream();
                if (stream.CanWrite)
                {
                    if (Str != null && Str != "")
                    {
                        string serverMsg = Str;
                        byte[] serverMsgAsByteArray = Encoding.UTF8.GetBytes(serverMsg);
                        stream.Write(serverMsgAsByteArray, 0, serverMsgAsByteArray.Length);
                        MsgBox += "Server: " + serverMsg + Environment.NewLine;
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show("Socket exception " + se.ToString());
            }
            catch (ArgumentNullException ae)
            {

            }
            Str = "";
        }

        private string Ip_;
        public string Ip
        {
            get { return Ip_; }

            set
            {
                Ip_ = value;
                Console.WriteLine($"IP: {Ip_}");
            }
        }

        private int Port_;
        public int Port
        {
            get { return Port_; }

            set
            {
                Port_ = value;
                Console.WriteLine($"Port: {Port_}");
            }
        }
        
        private void Connect(object obj)
        {
            try
            {
                thr = new Thread(new ThreadStart(ListenRequests));
                thr.IsBackground = true;
                thr.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Server connection exception " + e);
            }

        }


        public void DisplayMessage()
        {
            Console.WriteLine("You click the send button!");
        }

        private void ListenRequests()
        {
            try
            {
                //Set IP Address and Port Number
                //IPEndPoint ipLocalEndPoint = new IPEndPoint(IPAddress.Parse(Ip), 11000);
                listener = new TcpListener(IPAddress.Any, 11000); // Server should listen to all of the client activities on all network interface
                // Start listening for client requests
                listener.Start();
                
                // Buffer for reading data
                Byte[] bytes = new byte[1024];

                // Enter the listening loop
                while (true)
                {
                    //AcceptTcpClient = blocking method that is used to accept requests
                    using (client = listener.AcceptTcpClient())
                    {
                        MsgBox += "Client is connected" + Environment.NewLine;
                        // Get a stream object for reading and writing
                        using (NetworkStream stream = client.GetStream())
                        {

                            int length;
                            try
                            {
                                // Loop to receive all the data sent by the client
                                while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                                {
                                    var incomingData = new Byte[length];
                                    Array.Copy(bytes, 0, incomingData, 0, length);
                                    string clientMsg = Encoding.UTF8.GetString(incomingData);
                                    MsgBox += "Client said: " + clientMsg + Environment.NewLine;
                                }
                            }
                            catch (Exception e)
                            {
                                client.Close();
                                MsgBox += "connection close" + Environment.NewLine;
                            }
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show("Socket exception " + se.ToString());
            }
        }

        /* Try connect Listener to Client (2 tasks : IP Address and listener
        port try parse string with try and catch +if 
        

        // Convert IpAddress
        IPAddress ipAddress = IPAddress.Parse(Ip);

        // Create end point useing the above IPAddress to port
        IPEndPoint iPoint = new IPEndPoint(ipAddress, 5000);

        private void ServerStart()
        {
            TcpListener listener = new TcpListener(iPoint);
            listener.Start();
        } 
        */
    }
}

        /*

                try
        {
        int cport = 0;

        if (int.TryParse(port, out cport)
        }

    //Convert IP
    IPAddress ip = IPAddress.Parse(IPAddress);

    //convert port into int

    IPEndPoint Local() = new(IPAddress.parse(IPAddress), cport);
        
        TcpListener listener = new TcpListener(IPEndPoint);
    listener.Start();
        
*/
