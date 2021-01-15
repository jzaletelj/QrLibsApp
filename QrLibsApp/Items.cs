using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace QrLibsApp
{
    public class Items : INotifyPropertyChanged
    {
        private bool isSelected;
        private string name;
        private ImageSource imageIcon;
        private ObservableCollection<Items> subItems;

        public Items()
        {
        }

        public ObservableCollection<Items> SubItems

        {
            get { return subItems; }
            set
            {
                subItems = value;
                RaisedOnPropertyChanged("SubItems");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisedOnPropertyChanged("Name");
            }
        }

        public ImageSource ImageIcon
        {
            get { return imageIcon; }
            set
            {
                imageIcon = value;
                RaisedOnPropertyChanged("ImageIcon");
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisedOnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
    }
}
