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
        public Repository rep;
        public BLimp()
        {
            rep = new Repository();

        }
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

        public ObservableCollection<Product> Get_all_Products()
        {
            return rep.Get_all_Products();
        }
        public ObservableCollection<ScannedProduct> Get_all_ScannedProducts()
        {
            return rep.Get_all_Scans();
        }

        public  void add_product(Product product)
        {
             rep.add_Product(product);
            
        }

        public void add_ScannedProduct(ScannedProduct scan)
        {
             rep.add_ScannedProduct(scan);
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

        public ObservableCollection<ScannedProduct> getCurrentDayScannedProducts()
        {
            return rep.getCurrentDayScannedProducts(DateTime.Now);
        }
        public ObservableCollection<ScannedProduct> getScannedProductBetween2Days(DateTime dt1, DateTime dt2)
        {
            return rep.getScannedProductBetween2Days(dt1, dt2);

        }
        public int getOccurrencesOfNameInScansList(List<ScannedProduct> scans, string name)
        {
            return rep.getOccurrencesOfNameInScansList(scans, name);
        }

         

        public Dictionary<string, int> getProductsByDayStatistic(DateTime dt)
        {
            return rep.getProductsByDayStatistic(dt);
        }
        public Dictionary<string,int> getProductsBy2DatesStatistic(DateTime dt1, DateTime dt2)
        {
            return rep.getProductsBy2DaysStatistic(dt1, dt2);
        }
        public Dictionary<string, int> getProductsByMonthStatistic(DateTime dt)
        {
            return rep.getProductsByMonthStatistic(dt);
        }
        public Dictionary<string, int> getCategoryByDayStatistic(DateTime dt)
        {
            return rep.getCategoryByDayStatistic(dt);
        }
        public Dictionary<string, int> getCategotyBy2DatesStatistic(DateTime dt1,DateTime dt2)
        {
            return rep.getCategoryBy2DaysStatistic(dt1,dt2);
        }
        public Dictionary<string, int> getCategotyByMonthStatistic(DateTime dt1)
        {
            return rep.getCategoryByMonthStatistic(dt1);
        }
        public Dictionary<string, int> getStoresByDayStatistic(DateTime dt1)
        {
            return rep.getStoresByDayStatistic(dt1);
        }
        public Dictionary<string, int> getStoresBy2DatesStatistic(DateTime dt1,DateTime dt2)
        {
            return rep.getStoresBy2DaysStatistic(dt1,dt2);
        }
        public Dictionary<string, int> getStoresByMonthStatistic(DateTime dt1)
        {
            return rep.getStoresByMonthStatistic(dt1);
        }
        public Dictionary<string,float> getCostByDayStatistic()
        {
            return rep.getCostByDayStatistic();
        }
        public Dictionary<string, float> getCostBy2DatesStatistic(DateTime dt1,DateTime dt2)
        {
            return rep.getCostBy2DaysStatistic(dt1,dt2);
        }
        public Dictionary<string,float> getCostByMonthStatistic()
        {
            return rep.getCostByMonthStatistic();
        }
    }
}
