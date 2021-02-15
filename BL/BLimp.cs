using System;
using BE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BL
{
    public class BLimp
    {
        public List<Product> GetLastProducts()
        {
            return new List<Product> { new Product("fish", @"ssdasd", Category.food), new Product("meat", @"eergdfg", Category.hygene) };
            //return new Reposetory().Get_all_Products();
        }

        public void update_Product(Product product)
        {
            //new Reposetory().update_Product(product); 
        }
        public List<Product> Get_all_Products()
        {
            return new Repository().Get_all_Products();
        }
        public List<ScannedProduct> Get_all_ScannedProducts()
        {
            return new Repository().Get_all_Scans();
        }

        public void add_product(Product product)
        {
            new Repository().add_Product(product);
        }

    }
}
