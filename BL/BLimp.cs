using System;
using BE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLimp
    {
        public List<Product> GetLastProducts()
        {
            return new List<Product> { new Product(), new Product() };
            //return new Reposetory().Get_all_Products();
        }

        public void update_Product(Product product)
        {
            //new Reposetory().update_Product(product); 
        }

    }
}
