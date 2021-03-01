using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartShopping.PurchaseHistoryUC
{
    class PurchaseHistoryVM : INotifyPropertyChanged
    {
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

        public PurchaseHistoryV View;

        public PurchaseHistoryVM(PurchaseHistoryV view, ObservableCollection<ScannedProduct> products)
        {
            this.View = view;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            SourceList = products;

        }

        public PurchaseHistoryVM(PurchaseHistoryV view)
        {
            this.View = view;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }


        private Visibility _VisibilityProgressBar;
        public Visibility VisibilityProgressBar
        {
            get { return _VisibilityProgressBar; }
            set
            {
                _VisibilityProgressBar = value;
                OnPropertyChanged("VisibilityProgressBar");

            }
        }

        private Visibility _VisibilityListProducts;
        public Visibility VisibilityListProducts
        {
            get { return _VisibilityListProducts; }
            set
            {
                _VisibilityListProducts = value;
                OnPropertyChanged("VisibilityListProducts");

            }
        }
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            VisibilityProgressBar = Visibility.Visible;
            VisibilityListProducts = Visibility.Collapsed;
            SourceList = new PurchaseHistoryM().GetPurchaseHistoryList();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            VisibilityProgressBar = Visibility.Collapsed;
            VisibilityListProducts = Visibility.Visible;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
