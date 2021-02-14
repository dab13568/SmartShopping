using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Store: INotifyPropertyChanged
    {
        private Delegate[] InvocationList;
        private int _ID;
        private string _name;
        private Address _address;
         
        public event PropertyChangedEventHandler PropertyChanged;

        public Store(int id,string name,Address address)
        {
            this._ID = id;
            this._name = name;
            this._address = address;

        }

        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged("ID");
            }
        }

        public string name
        {
            get { return _name; }
            set
            {
                _name= value;
                OnPropertyChanged("name");
            }
        }
        public Address address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("address");
            }
        }

        protected void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        public void ClearPropertyChanged() { InvocationList = PropertyChanged.GetInvocationList(); PropertyChanged = null; }
        public void RestorePropertyChanged()
        {
            foreach (var item in InvocationList)
            {
                PropertyChanged += (PropertyChangedEventHandler)item;
            }
        }



    }


}
