using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.MainWindowMVVM
{
    class MainWindowVM : INotifyPropertyChanged
    {

        private  MainWindow View;

        public MainWindowVM(MainWindow mainWindow)
        {
            View = mainWindow;
        }









        public event PropertyChangedEventHandler PropertyChanged;
    }
}
