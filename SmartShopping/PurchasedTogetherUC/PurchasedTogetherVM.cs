using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartShopping.PurchasedTogetherUC
{
    class PurchasedTogetherVM : INotifyPropertyChanged
    {
        
        public PurchasedTogetherUserControlV View;

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


        private Dictionary<Product,float> _ProbabilityDict;
        public Dictionary<Product, float> ProbabilityDict
        {
            get { return _ProbabilityDict; }
            set
            {
                _ProbabilityDict = value;
                OnPropertyChanged("ProbabilityDict");

                if (_ProbabilityDict.Count == 0)
                    VisibilityLabelNothingToShow = Visibility.Visible;
                else VisibilityLabelNothingToShow = Visibility.Collapsed;
            }
        }


        public readonly BackgroundWorker worker = new BackgroundWorker();

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

        public PurchasedTogetherVM(PurchasedTogetherUserControlV view)
        {
            this.View = view;
            SourceList = new PurchaseTogetherM().GetProductList();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            VisibilityProgressBar = Visibility.Visible;
            VisibilityListProducts = Visibility.Collapsed;
            if (SelectedProduct != null) 
                ProbabilityDict = new PurchaseTogetherM().getProbabilityDidt(SelectedProduct.num);
        }
        
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            VisibilityProgressBar = Visibility.Collapsed;
            VisibilityListProducts = Visibility.Visible;
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


        public void loadProbabilityDict()
        {
            worker.RunWorkerAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "SelectedProduct")
                loadProbabilityDict();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}

