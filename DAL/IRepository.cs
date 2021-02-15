using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IRepository
    {
        List<ScannedProduct> Get_all_Scans();
        List<Product> Get_all_Products();
        List<Store> get_all_Stores();

        void add_Product(Product product);
        void add_ScannedProduct(ScannedProduct scan);

        void update_Product(Product product);
        void update_ScannedProduct(ScannedProduct scan);

        void delete_Product(Product product);
        void delete_ScannedProduct(ScannedProduct scan);


    }
}
