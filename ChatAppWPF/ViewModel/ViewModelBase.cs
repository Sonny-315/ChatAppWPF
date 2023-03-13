using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ChatAppWPF.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        string _str;
        public string Str
        {
            get
            {
                return _str;
            }
            set
            {
                if (_str == value)
                    return;
                _str = value;
                OnPropertyChanged("Str");
            }
        }

        string _msgBox;
        public string MsgBox
        {
            get
            {
                return _msgBox;
            }
            set
            {
                if (_msgBox == value)
                    return;
                _msgBox = value;
                OnPropertyChanged("MsgBox");
            }
        }

        bool _isConnected = false;
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                if (_isConnected == value)
                    return;
                _isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }
    }
}
