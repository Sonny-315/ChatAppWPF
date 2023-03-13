using ChatAppWPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace ChatAppWPF.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        TcpClient client;
        Thread receiveThr;
        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand SendCommand { get; set; }
        public MainWindowViewModel()
        {
            ConnectCommand = new RelayCommand(Connect);
            SendCommand = new RelayCommand(Send);
        }

        private void Send(object obj)
        {
            if (client == null)
            {
                return;
            }
            try
            {
                NetworkStream stream = client.GetStream();
                if (stream.CanWrite)
                {
                    if (Str != null && Str != "")
                    {
                        string clientMsg = Str;
                        byte[] clientMsgAsByteArray = Encoding.UTF8.GetBytes(clientMsg);
                        stream.Write(clientMsgAsByteArray, 0, clientMsgAsByteArray.Length);
                        MsgBox += "Client: " + clientMsg + Environment.NewLine;
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show("Socket exception: " + se);
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
        private int Id_;
        public int Id
        {
            get { return Id_; }

            set
            {
                Id_= value;
                Console.WriteLine($"ID: {Id_}");
            }
        }

        private void Connect(object obj)
        {
            try
            {
                receiveThr = new Thread(new ThreadStart(ListenData));
                receiveThr.IsBackground = true;
                receiveThr.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Client connection exception " + e);
            }
        }

        private void ListenData()
        {
            try
            {

                IPEndPoint ipLocalEndPoint = new IPEndPoint(IPAddress.Parse(Ip), 11000);
                client = new TcpClient(ipLocalEndPoint);
                IPAddress ip = IPAddress.Parse(Ip);
                client.Connect(ip, Port);
                if (client.Connected == true)
                {
                    MsgBox += "Client is connected!" + Environment.NewLine;
                    Byte[] bytes = new Byte[1024];
                    while (true)
                    {
                        using (NetworkStream stream = client.GetStream())
                        {
                            int length;
                            try
                            {
                                while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                                {
                                    var incommingData = new byte[length];
                                    Array.Copy(bytes, 0, incommingData, 0, length);
                                    string serverMsg = Encoding.UTF8.GetString(incommingData);
                                    MsgBox += "Server: " + serverMsg + Environment.NewLine;
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
                MessageBox.Show("Socket exception: " + se);
            }
        }

    }
}
