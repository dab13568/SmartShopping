using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IRepository
    {
        ObservableCollection<ScannedProduct> Get_all_Scans();
        ObservableCollection<Product> Get_all_Products();
        ObservableCollection<Store> get_all_Stores();

        void add_Product(Product product);
        void add_ScannedProduct(ScannedProduct scan,string name);

        void update_Product(Product product);
        void update_ScannedProduct(ScannedProduct scan);

        void delete_Product(Product product);
        void delete_ScannedProduct(ScannedProduct scan);


    }
}