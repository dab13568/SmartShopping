using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SmartShopping.MainWindowMVVM
{
    class MainWindowVM : INotifyPropertyChanged
    {
       

        private  MainWindow View;

        public MainWindowVM(MainWindow mainWindow)
        {
            View = mainWindow;
            new BLimp().connectSqlServer();
            View.LoadHomeView();

        }

        private Visibility _VisibilityOpenMenu;
        public Visibility VisibilityOpenMenu
        {
            get { return _VisibilityOpenMenu; }
            set
            {
                _VisibilityOpenMenu = value;
                OnPropertyChanged("VisibilityOpenMenu");

            }
        }

        private Visibility _VisibilityCloseMenu=Visibility.Collapsed;
        public Visibility VisibilityCloseMenu
        {
            get { return _VisibilityCloseMenu; }
            set
            {
                _VisibilityCloseMenu = value;
                OnPropertyChanged("VisibilityCloseMenu");

            }
        }


        public ICommand HomeView { get { return new LoadHomeCMD(this); } }

        public ICommand LastProductsView { get { return new LastProductsCMD(this); } }

        public ICommand PurchaseHistoryView { get { return new PurchaseHistoryCMD(this); } }

        public ICommand PurchasedTogetherView { get { return new PurchasedTogetherCMD(this); } }


        public ICommand RecommendedShoppingView { get { return new RecommendedShoppingCMD(this); } }

        public ICommand StatisticsView { get { return new StatisticsCMD(this); } }

        public ICommand loadCloseMenuCMD { get { return new closeMenuCMD(this); } }
        public ICommand loadOpenMenuCMD { get { return new openMenuCMD(this); } }



        public void LoadHomeView() { View.LoadHomeView(); }

        public void LoadLastProductsView() { View.LoadLastProductsView(); }

        public void LoadPurchaseHistoryView() { View.LoadPurchaseHistoryView(); }

        public void LoadStatisticsView() { View.LoadStatisticsView(); }

        public void LoadRecommendedShoppingView(){ View.LoadRecommendedShoppingView(); }

        public void LoadPurchaseTogetherView() { View.LoadPurchaseTogetherView(); }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
