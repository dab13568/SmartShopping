using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class Repository: IRepository
    {
        public Repository() { }

        public void add_Product(Product product)
        {
            using (var context = new ProductDB())
            {
                context.products.Add(product);
                context.SaveChanges();
            }
        }

        public void add_ScannedProduct(ScannedProduct scan)
        {
            //using (var context = new ProductDB())
            //{
            //    context.scans.Add(scan);
            //    context.SaveChanges();
            //}
        }

        public void delete_Product(Product product)
        {
            throw new NotImplementedException();
        }

        public void delete_ScannedProduct(ScannedProduct scan)
        {
            throw new NotImplementedException();
        }

        public List<Product> Get_all_Products()
        {
            throw new NotImplementedException();
        }

        public List<ScannedProduct> Get_all_Scans()
        {
            throw new NotImplementedException();
        }

        public void update_Product(Product product)
        {
            throw new NotImplementedException();
        }

        public void update_ScannedProduct(ScannedProduct scan)
        {
            throw new NotImplementedException();
        }
    }
}
