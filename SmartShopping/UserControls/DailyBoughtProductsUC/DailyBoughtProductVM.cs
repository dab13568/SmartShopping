using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace SmartShopping.UserControls.DailyBoughtProductsUC
{
    class DailyBoughtProductVM : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products{ get; set;}
        public DailyBoughtProductVM()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            p1._name = "kola";
            //p1.
            Products.Add(new Product { })
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
