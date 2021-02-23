using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class Repository : IRepository 
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
            using (var context = new ProductDB())
            {
               
                var arr=(from p in context.products where p.num==scan.productNo select p ).ToList<Product>();
                if (arr.Count == 0)
                {
                    add_Product(new Product(scan.productNo, "", "", null));

                }
                var arr2 = (from p in context.scans where p.productNo == scan.productNo && p.store.Equals(scan.store) &&  (0==scan.dateScan.Date.CompareTo(p.dateScan.Date)) select p).ToList<ScannedProduct>();
                if(arr2.Count!=0)
                {
                    arr2.ElementAt(0).amount++;
                }
                if (arr2.Count == 0)
                {
                    context.scans.Add(scan);
                    context.SaveChanges();
                }
            }
        }

        public void delete_Product(Product product)
        {
            using (var context = new ProductDB())
            {
                var old = context.products.Find(product.productID);
                context.products.Remove(old);

                context.SaveChanges();
            }
        }

        public void delete_ScannedProduct(ScannedProduct scan)
        {
            using (var context = new ProductDB())
            {
                var old = context.scans.Find(scan.productNo);
                context.scans.Remove(old);

                context.SaveChanges();
            }
        }

        public List<Product> Get_all_Products()
        {
            List<Product> result = new List<Product>();
            using (var context = new ProductDB())
            {
                result = (from p in context.products select p).ToList<Product>();
            }
            return result;
        }

        public List<ScannedProduct> Get_all_Scans()
        {
            List<ScannedProduct> result = new List<ScannedProduct>();
            using (var context = new ProductDB())
            {
                result = (from p in context.scans select p).ToList<ScannedProduct>();
            }
            return result;
        }

        public List<Store> get_all_Stores()
        {
            throw new NotImplementedException();
        }

        public void update_Product(Product product)
        {
            using (var context = new ProductDB())
            {
                var old = context.products.Find(product.productID);
                old.name = product.name;
                old.imageUrl = product.imageUrl;

                context.SaveChanges();
            }
        }

        public void update_ScannedProduct(ScannedProduct scan)
        {
            using (var context = new ProductDB())
            {
                var old = context.scans.Find(scan.Id);
                old.amount = scan.amount;
                old.cost = scan.cost;
                old.dateScan = scan.dateScan;
               
                //old.productName = scan.productName;
                old.rating = scan.rating;
                old.store = scan.store;

                context.SaveChanges();
            }
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
    }
}