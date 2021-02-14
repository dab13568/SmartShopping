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
        private Delegate[] InvocationList;

        private int _orderID;
        private int _productId;
        private Store _store;
        private DateTime _dateScan;
        private float _cost;
        private int _amount;
        private int? _rating;

        public ScannedProduct(int order,int productId,Store store,DateTime time,float cost,int amount)
        {
            this._orderID = order;
            this._productId = productId;
            this._store = store;
            this._dateScan = time;
            this._cost = cost;
            this._amount = amount;
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
        public int orderID
        {
            get { return _orderID; }
            set
            {
                _orderID = value;
                OnPropertyChanged("orderID");
            }
        }

        public int productId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                OnPropertyChanged("productId");
            }
        }
        public Store storeId
        {
            get { return _store; }
            set
            {
                _store = value;
                OnPropertyChanged("storeId");
            }
        }
        public DateTime dateScan
        {
            get { return _dateScan; }
            set
            {
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
