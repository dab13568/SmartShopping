using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SmartShopping.ListProductsUCMVVM;
using SmartShopping.EditProductWindow;

namespace SmartShopping.LastProductsUC
{
    public class LastProductsUserControlVM :  INotifyPropertyChanged, ILoadEditWindow
    {
        public readonly BackgroundWorker worker = new BackgroundWorker();

        private Visibility _VisibilityProgressBar;
        public Visibility  VisibilityProgressBar
        {
            get { return _VisibilityProgressBar; }
            set
            {
                _VisibilityProgressBar = value;
                OnPropertyChanged("VisibilityProgressBar");

            }
        }

        private Visibility _VisibilityLabelNothingToShow;
        public Visibility VisibilityLabelNothingToShow
        {
            get { return _VisibilityLabelNothingToShow; }
            set
            {
                _VisibilityLabelNothingToShow = value;
                OnPropertyChanged("VisibilityLabelNothingToShow");

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

        private ObservableCollection<ScannedProduct> _SourceList;
        public  ObservableCollection<ScannedProduct> SourceList
        {
            get { return _SourceList; }
            set
            {
                _SourceList = value;
                OnPropertyChanged("SourceList");
                if (_SourceList.Count == 0) 
                    VisibilityLabelNothingToShow = Visibility.Visible;
                else VisibilityLabelNothingToShow = Visibility.Collapsed;
            }
        }

        public ICommand EditProductView { get { return new LoadEditProductCMD(this); } }
        public ICommand DeleteProductView { get { return new LoadDeleteProductCMD(this); } }


        public LastProductsUserControlV View;
        public LastProductsUserControlVM(LastProductsUserControlV view, ObservableCollection<ScannedProduct> scannedProduct)
        {
            this.View = view;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            SourceList = scannedProduct;

        }


        public void LoadEditProductView(EditProduct ep)
        {
            View.loadEditProductView(ep);
            worker.RunWorkerAsync();
        }

        public LastProductsUserControlVM(LastProductsUserControlV view)
        {
            this.View = view;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            worker.RunWorkerAsync();

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            VisibilityProgressBar = Visibility.Visible;
            VisibilityListProducts = Visibility.Collapsed;
            SourceList = new LastProductsUserControlM().GetLastProducts();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            VisibilityProgressBar = Visibility.Collapsed; 
            VisibilityListProducts = Visibility.Visible;
        }

        // INotifyPropertyChanged implementaion
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        
    }
}
