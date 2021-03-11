using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BE
{ 
    public class ScannedProduct:INotifyPropertyChanged
    {
        private int _Id;
        private int _productNo;
        private string _store;
        private DateTime _dateScan;
        private float _cost;
        private int _amount;
        private int? _rating;


        public ScannedProduct(int no,string store,DateTime time,float cost,int amount=1)
        { 
            this._store = store;
            this._dateScan = time;
            this._cost = cost;
            this._amount = amount;
            this._productNo = no;
        }

        public ScannedProduct() { }


        public int productNo
        {
            get { return _productNo; }
            set
            {
                _productNo = value;
                OnPropertyChanged("productNo");
            }
        }
        public int? rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged("rating");
            }
        }

        public int amount
        {
            get { return _amount; }
            set { _amount = value;
                OnPropertyChanged("amount");
            }
        }
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string store
        {
            get { return _store; }
            set
            {
                _store = value;
                OnPropertyChanged("store");
            }
        }
        public DateTime dateScan
        {
            get { return _dateScan; }
            set
            {
                string s=_dateScan.ToShortDateString();
                _dateScan = value;
                OnPropertyChanged("dateScan");
            }
        }
        public float cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged("cost");
            }
        }







        public event PropertyChangedEventHandler PropertyChanged;
        

        public void OnPropertyChanged(string propertyName) {
            //if(PropertyChanged!=null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}
