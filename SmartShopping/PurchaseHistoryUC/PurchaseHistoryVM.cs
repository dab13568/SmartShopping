using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.PurchaseHistoryUC
{
    class PurchaseHistoryVM : INotifyPropertyChanged
    {
        private List<Product> _PurchaseHistoryList;
        public List<Product> PurchaseHistoryList
        {
            get { return _PurchaseHistoryList; }
            set
            {
                _PurchaseHistoryList = value;
                OnPropertyChanged("ProductsList");
            }
        }

        public PurchaseHistoryV View;

        public PurchaseHistoryVM(PurchaseHistoryV view, List<Product> products)
        {
            this.View = view;
            PurchaseHistoryList = products;

        }

        public PurchaseHistoryVM(PurchaseHistoryV view)
        {
            this.View = view;
            PurchaseHistoryList = new PurchaseHistoryM().GetPurchaseHistoryList();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
