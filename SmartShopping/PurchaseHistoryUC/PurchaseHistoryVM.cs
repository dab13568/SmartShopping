﻿using BE;
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

        public PurchaseHistoryV View;

        public PurchaseHistoryVM(PurchaseHistoryV view, List<Product> products)
        {
            this.View = view;
            SourceList = products;

        }

        public PurchaseHistoryVM(PurchaseHistoryV view)
        {
            this.View = view;
            SourceList = new PurchaseHistoryM().GetPurchaseHistoryList();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
