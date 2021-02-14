using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartShopping.MainWindowMVVM
{
    class MainWindowVM : INotifyPropertyChanged
    {
       

        private  MainWindow View;

        public MainWindowVM(MainWindow mainWindow)
        {
            View = mainWindow;
        }


        public ICommand HomeView { get { return new LoadHomeCMD(this); } }

        public ICommand LastProductsView { get { return new LastProductsCMD(this); } }

        public ICommand PurchasedTogetherView { get { return new PurchasedTogetherCMD(this); } }

        public ICommand RecommendedShoppingView { get { return new RecommendedShoppingCMD(this); } }

        public ICommand StatisticsView { get { return new StatisticsCMD(this); } }

        public ICommand SelectedItemChangedCommand { get { return new MenuCMD(this); } }

        public void LoadHomeView() { View.LoadHomeView(); }

        public void LoadLastProductsView() { View.LoadLastProductsView(); }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
