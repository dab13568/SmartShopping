using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.PurchasedTogetherUC
{
    class PurchasedTogetherVM : INotifyPropertyChanged
    {
        private List<Product> _SourceList;
        public List<Product> SourceList
        {
            get { return _SourceList; }
            set
            {
                _SourceList = value;
                OnPropertyChanged("SourceList");
            }
        }

        public PurchasedTogetherUserControlV View;

        public PurchasedTogetherVM(PurchasedTogetherUserControlV view, List<Product> products)
        {
            this.View = view;
            SourceList = products;

        }

        public PurchasedTogetherVM(PurchasedTogetherUserControlV view)
        {
            this.View = view;
            SourceList = new PurchaseTogetherM().GetPurchaseTogetherList();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}

