using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                if (product.imageUrl == "")
                    product.imageUrl = "pack://application:,,,/Images/defaultImg.jpg";
                context.products.Add(product);
                context.SaveChanges();
            } 
        }

        public void add_ScannedProduct(ScannedProduct scan   )
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
                var old = context.scans.Find(scan.Id);
                context.scans.Remove(old);

                context.SaveChanges();
            }
        }

        public ObservableCollection<Product> Get_all_Products()
        {
            List<Product> result = new List<Product>();
            using (var context = new ProductDB())
            {
                result = (from p in context.products select p).ToList<Product>();
            }
            return new ObservableCollection<Product>(result);
        }

        public ObservableCollection<ScannedProduct> Get_all_Scans()
        {
            //call drive
            List<ScannedProduct> result = new List<ScannedProduct>();
            using (var context = new ProductDB())
            {
                result = (from p in context.scans select p).ToList<ScannedProduct>();
            }
            return new ObservableCollection<ScannedProduct>(result) ;
        }

        public ObservableCollection<Store> get_all_Stores()
        {
            throw new NotImplementedException();
        }

        public void update_Product(Product product)
        {
            using (var context = new ProductDB())
            {
                var old = context.products.Find(product.productID);
                old.name = product.name;
                old.num = product.num;
                old.imageUrl = product.imageUrl;
                old.category = product.category;
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

        public ObservableCollection<ScannedProduct> getCurrentDayScannedProducts(DateTime dt)
        {
            List<ScannedProduct> result;
            //value.dateScan.ToShortDateString().Equals(dt.ToShortDateString())
            using (var context = new ProductDB())
            {
                result = (from p in context.scans where p.dateScan.ToShortDateString().Equals(dt.ToShortDateString()) select p).ToList<ScannedProduct>();
            }
            return new ObservableCollection<ScannedProduct>(result);
        }
        public ObservableCollection<ScannedProduct> getScannedProductBetween2Days(DateTime dt1, DateTime dt2)
        {
            List<ScannedProduct> result;
            //value.dateScan.ToShortDateString().Equals(dt.ToShortDateString())
            using (var context = new ProductDB())
            {
                result = (from p in context.scans where p.dateScan.Date >= dt1 && p.dateScan.Date <= dt2 select p).ToList<ScannedProduct>();
            }
            return new ObservableCollection<ScannedProduct>(result);

        }
        public int getOccurrencesOfNameInScansList(List<ScannedProduct> scans, string name)
        {
            int count = 0;
            using (var context = new ProductDB())
            {
                foreach (var scan in scans)
                {

                    if (name.Equals(context.products.FirstOrDefault(value => value.num == scan.productNo).name))
                        count++;
                }
            }
            return count;
        }

        public Dictionary<string, int> getProductsByDayStatistic(DateTime dt)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string name = "";
            using (var context = new ProductDB())
            {
                foreach (var productNum in (from p in context.scans where p.dateScan.Date == dt.Date select p.productNo).ToList<int>())
                {
                    name = context.products.FirstOrDefault(value => value.num == productNum).name;
                    if (!dict.ContainsKey(name))
                        dict[name] = 0;
                    dict[name]++;
                }
            }
            return dict;
        }

        public Dictionary<string, int> getCategoryByDayStatistic(DateTime dt)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string name = "";
            using (var context = new ProductDB())
            {
                foreach (var productNum in (from p in context.scans where p.dateScan.Date == dt.Date select p.productNo).ToList<int>())
                {
                    name = context.products.FirstOrDefault(value => value.num == productNum).category.ToString();
                    if (!dict.ContainsKey(name))
                        dict[name] = 0;
                    dict[name]++;
                }
            }
            return dict;
        }

        public Dictionary<string, int> getStoresByDayStatistic(DateTime dt)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            using (var context = new ProductDB())
            {
                foreach (var product in (from p in context.scans where p.dateScan.Date == dt.Date select p).ToList<ScannedProduct>())
                {
                    //name = context.products.FirstOrDefault(value => value.num == productNum)..ToString();
                    if (!dict.ContainsKey(product.store))
                        dict[product.store] = 0;
                    dict[product.store]++;
                }
            }
            return dict;
        }

        public float getCostByDayStatistic(DateTime dt)
        {
            float cost=0;
            using (var context = new ProductDB())
            {
                foreach (var product in (from p in context.scans where p.dateScan.Date == dt.Date select p).ToList<ScannedProduct>())
                {
                    cost += product.cost * product.amount;
                }
            }
            return cost;
        }


        public Dictionary<string, int> getProductsBy2DaysStatistic(DateTime dt1, DateTime dt2)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string name ;
            
            using (var context = new ProductDB())
            {
                foreach (var product in getScannedProductBetween2Days(dt1, dt2))
                {
                    
                    name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                    if (!dict.ContainsKey(name))
                        dict[name] = 0;
                    dict[name]++;
                }
            }
            return dict;
        }

        public Dictionary<string, int> getCategoryBy2DaysStatistic(DateTime dt1, DateTime dt2)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string name;

            using (var context = new ProductDB())
            {
                foreach (var product in getScannedProductBetween2Days(dt1, dt2))
                {

                   name = context.products.FirstOrDefault(value => value.num == product.productNo).category.ToString();

                    if (!dict.ContainsKey(name))
                        dict[name] = 0;
                    dict[name]++;
                }
            }
            return dict;
        }

        public Dictionary<string, int> getStoresBy2DaysStatistic(DateTime dt1, DateTime dt2)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            using (var context = new ProductDB())
            {
                foreach (var product in getScannedProductBetween2Days(dt1, dt2))
                {

                    //name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                    if (!dict.ContainsKey(product.store))
                        dict[product.store] = 0;
                    dict[product.store]++;
                }
            }
            return dict;
        }


        public float getCostBy2DaysStatistic(DateTime dt1, DateTime dt2)
        {
            float cost = 0;
            
            using (var context = new ProductDB())
            {
                foreach (var product in getScannedProductBetween2Days(dt1, dt2))
                {

                    //name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                    cost += product.cost * product.amount;
                }
            }
            return cost;
        }


        public Dictionary<string, int> getProductsByMonthStatistic(DateTime dt)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string name = "";

            using (var context = new ProductDB())
            {
                
                foreach (var product in (from p in context.scans where p.dateScan.Month == dt.Month select p).ToList<ScannedProduct>())
                {
                    name = context.products.FirstOrDefault(value => value.num == product.productNo).name;
                    if (!dict.ContainsKey(name))
                        dict[name] = 0;
                    dict[name]++;
                }
            }
            return dict;
        }
        public Dictionary<string, int> getCategoryByMonthStatistic(DateTime dt)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string name;

            using (var context = new ProductDB())
            {
                foreach (var product in (from p in context.scans where p.dateScan.Month == dt.Month select p).ToList<ScannedProduct>())
                {

                    name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                    if (!dict.ContainsKey(name))
                        dict[name] = 0;
                    dict[name]++;
                }
            }
            return dict;
        }

        public Dictionary<string, int> getStoresByMonthStatistic(DateTime dt)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            using (var context = new ProductDB())
            {
                foreach (var product in (from p in context.scans where p.dateScan.Month == dt.Month select p).ToList<ScannedProduct>())
                {

                    //name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                    if (!dict.ContainsKey(product.store))
                        dict[product.store] = 0;
                    dict[product.store]++;
                }
            }
            return dict;
        }

        public float getCostByMonthStatistic(DateTime dt)
        {
            float cost = 0;

            using (var context = new ProductDB())
            {
                foreach (var product in (from p in context.scans where p.dateScan.Month == dt.Month select p).ToList<ScannedProduct>())
                {

                    //name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                    cost += product.cost * product.amount;
                }
            }
            return cost;
        }

    }
}