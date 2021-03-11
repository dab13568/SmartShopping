using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SmartShopping.RecommendedShoppingUC
{
    class RecommendedShoppingUserControlVM : INotifyPropertyChanged
    {
        public readonly BackgroundWorker worker = new BackgroundWorker();
        bool inProggress = false;
        private Visibility _VisibilityProgressBar = Visibility.Collapsed;
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

        private List<string> _SourceDaysList;
        public List<string> SourceDaysList
        {
            get { return _SourceDaysList; }
            set
            {
                _SourceDaysList = value;
                OnPropertyChanged("SourceDaysList");
            }
        }

        private string _SelectedDay;
        public string SelectedDay
        {
            get { return _SelectedDay; }
            set
            {
                _SelectedDay = value;
                OnPropertyChanged("SelectedDay");
            }
        }

        public RecommendedShoppingUserControlV View;

        public RecommendedShoppingUserControlVM(RecommendedShoppingUserControlV view)
        {
            this.View = view;
            SourceDaysList = new RecommendedShoppingUserControlM().getDaysWhichHeBought();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }


        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            inProggress = true;
            VisibilityProgressBar = Visibility.Visible;
            VisibilityListProducts = Visibility.Collapsed;
            if(SelectedDay != null)
                SourceList = new RecommendedShoppingUserControlM().GetRecommendedShoppingList(SelectedDay);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            VisibilityProgressBar = Visibility.Collapsed;
            VisibilityListProducts = Visibility.Visible;
            inProggress = false;
        }


        public void loadRecommendedProducts()
        {
            if(!inProggress)
            worker.RunWorkerAsync();

        }

        public ICommand loadPdfView { get { return new loadPDFrecommendedCMD(this); } }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) 
        {
            if (propertyName == "SelectedDay")
                loadRecommendedProducts();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
