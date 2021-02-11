using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BE;

namespace SmartShopping.UserControls.DailyBoughtProductsUC
{
    class DailyBoughtProductVM : INotifyPropertyChanged
    {

        public DailyBoughtProductVM(DailyBoughtProducts view)
        {
            Products = new ObservableCollection<Product>();
            Product p1 = new Product();
            Product p2 = new Product();
            p1.name = "kola";
            //p1._imageUrl = "../../Images/background.jpg";
            p2.name = "water";
            //p2._imageUrl = "../../Images/background.jpg";


            Products.Add(p1);
            Products.Add(p2);


        }
        public ObservableCollection<Product> Products { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

      
    }
}
