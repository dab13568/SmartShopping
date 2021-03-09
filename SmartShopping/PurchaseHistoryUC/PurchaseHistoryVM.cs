using BE;
using BL;
using SmartShopping.EditProductWindow;
using SmartShopping.ListProductsUCMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SmartShopping.PurchaseHistoryUC
{
    class PurchaseHistoryVM : INotifyPropertyChanged, ILoadEditWindow
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
        public ICommand EditProductView  { get { return new LoadEditProductCMD(this); } }

        

        public PurchaseHistoryV View;
        public PurchaseHistoryVM(PurchaseHistoryV view)
        {
            this.View = view;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        public void changeView()
        {

            if (firstDate != null && secondDate != null && firstDate <= secondDate)
            {

                SourceList = new PurchaseHistoryM().GetPurchaseHistoryList(firstDate,secondDate);
            }
            if (firstDate > secondDate)
                View.InvalidDates.Visibility = Visibility.Visible;
            else View.InvalidDates.Visibility = Visibility.Collapsed;
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

        private DateTime _firstDate = DateTime.Now.AddDays(-14);
        public DateTime firstDate
        {
            get { return _firstDate; }
            set
            {
                _firstDate = value;
                OnPropertyChanged("firstDate");
            }
        }

        private DateTime _secondDate = DateTime.Now;
        public DateTime secondDate
        {
            get { return _secondDate; }
            set
            {
                _secondDate = value;
                OnPropertyChanged("firstDate");
            }
        }

        public void LoadEditProductView(EditProduct ep)
        {
            View.loadEditProductView(ep);
        }


        private readonly BackgroundWorker worker = new BackgroundWorker();
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            VisibilityProgressBar = Visibility.Visible;
            VisibilityListProducts = Visibility.Collapsed;
            SourceList = new PurchaseHistoryM().GetPurchaseHistoryList(firstDate,secondDate);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            VisibilityProgressBar = Visibility.Collapsed;
            VisibilityListProducts = Visibility.Visible;
        }




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "firstDate" || propertyName == "secondDate")
                changeView();
            else PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
