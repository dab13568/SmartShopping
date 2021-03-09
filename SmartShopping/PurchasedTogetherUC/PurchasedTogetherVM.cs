using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.PurchasedTogetherUC
{
    class PurchasedTogetherVM : INotifyPropertyChanged
    {
        
        public PurchasedTogetherUserControlV View;

        public PurchasedTogetherVM(PurchasedTogetherUserControlV view, ObservableCollection<ScannedProduct> products)
        {
            this.View = view;
            SourceList = products;

        }

        private ObservableCollection<ScannedProduct> _SourceList;
        public ObservableCollection<ScannedProduct> SourceList
        {
            get { return _SourceList; }
            set
            {
                _SourceList = value;
                OnPropertyChanged("SourceList");
            }
        }
        public PurchasedTogetherVM(PurchasedTogetherUserControlV view)
        {
            this.View = view;
            //SourceList = new PurchaseTogetherM().GetPurchaseTogetherList();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}

