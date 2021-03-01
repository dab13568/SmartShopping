using System;
using BE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BL
{
    public class BLimp
    {
        //public List<Product> GetLastProducts()
        //{
        //    //return new List<Product> { new Product(2,"fish",@"ssdasd",Category.food), new Product(7,"meat",@"eergdfg",Category.hygene) };
        //    //return new Reposetory().Get_all_Products();
        //}

        public void update_Product(Product product)
        {
            if (product !=null)
            new Repository().update_Product(product); 
        }

        public void update_ScannedProduct(ScannedProduct sp)
        {
            if (sp != null)
                new Repository().update_ScannedProduct(sp);
        }

        public List<Product> Get_all_Products()
        {
            return new Repository().Get_all_Products();
        }
        public ObservableCollection<ScannedProduct> Get_all_ScannedProducts()
        {
            return new Repository().Get_all_Scans();
        }

        public  void add_product(Product product)
        {
            new Repository().add_Product(product);
        }

        public void add_ScannedProduct(ScannedProduct scan)
        {
            new Repository().add_ScannedProduct(scan);
        }

        public string getImageUrlByProductId(int id)
        {
            string result = "";
            using (var context = new ProductDB())
            {
                result = context.products.FirstOrDefault(value => value.num == id).imageUrl;
            }
            return result;
        }

        public string getCategoryStringByProductId(int id)
        {
            string result = "";
            using (var context = new ProductDB())
            {
                result = context.products.FirstOrDefault(value => value.num == id).category.ToString();
            }
            return result;
        }

        public string getNameByProductId(int id)
        {
            string result = "";
            using (var context = new ProductDB())
            {
                result = context.products.FirstOrDefault(value => value.num == id).name;
            }
            return result;
        }

        public Product getProductByScannedProduct(ScannedProduct sp)
        {
            Product result;
            using (var context = new ProductDB())
            {
                result = context.products.FirstOrDefault(value => value.num == sp.productNo);
            }
            return result;
        }
    }
}
