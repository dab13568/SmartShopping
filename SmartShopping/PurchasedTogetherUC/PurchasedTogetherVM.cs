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

        public PurchasedTogetherVM(PurchasedTogetherUserControlV view, ObservableCollection<Product> products)
        {
            this.View = view;
            SourceList = products;

        }

        private ObservableCollection<Product> _SourceList;
        public ObservableCollection<Product> SourceList
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
            SourceList = new PurchaseTogetherM().GetPurchaseTogetherList();
        }

        private Product _SelectedProduct;
        public Product SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }


        public load

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(propertyName == "SourceList")

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}

