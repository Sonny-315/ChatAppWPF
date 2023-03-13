using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatAppWPF.Model
{

    public class LoginViewModel
    {
        private string ip_;
        public string ip 
        {
            get { return ip_; }

            set 
            { 
                ip_ = value;
                Console.WriteLine($"IP: {ip_}");
            } 
        }

        private int port_;
        public int port 
        {
            get { return port_; }
            
            set
            {
                port_ = value;
                Console.WriteLine($"Port: {port_}");
            }
        }

        private string id_;
        public string id 
        { 
            get { return id_; } 
            set
            {
                id_ = value;
                Console.WriteLine($"ID: {id_}");
            }
        }

        public LoginViewModel()
        { 
        }
    }
}
