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

        public ICommand SelectedItemChangedCommand { get { return new MenuCMD(this); } }


        public void LoadHomeView() { View.LoadHomeView(); }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
