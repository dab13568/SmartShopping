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
            rep.update_Product(product); 
        }

        public void update_ScannedProduct(ScannedProduct sp)
        {
            if (sp != null)
                rep.update_ScannedProduct(sp);
        }
        public void delete_ScannedProduct(ScannedProduct s)
        {
            rep.delete_ScannedProduct(s);
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

        public Dictionary<string, float> getStatisticData(int subject, int timePeriod, DateTime? dt1=null, DateTime? dt2=null)
        {
            return rep.getStatisticData(subject, timePeriod, dt1, dt2);
        }

        public Dictionary<string, float> getProductsByDayStatistic(DateTime dt)
        {
            return rep.getProductsByDayStatistic(dt);
        }
        public Dictionary<string,float> getProductsBy2DatesStatistic(DateTime dt1, DateTime dt2)
        {
            return rep.getProductsBy2DaysStatistic(dt1, dt2);
        }
        public Dictionary<string, float> getProductsByMonthStatistic(DateTime dt)
        {
            return rep.getProductsByMonthStatistic(dt);
        }
        public Dictionary<string, float> getCategoryByDayStatistic(DateTime dt)
        {
            return rep.getCategoryByDayStatistic(dt);
        }
        public Dictionary<string, float> getCategotyBy2DatesStatistic(DateTime dt1,DateTime dt2)
        {
            return rep.getCategoryBy2DaysStatistic(dt1,dt2);
        }
        public Dictionary<string, float> getCategotyByMonthStatistic(DateTime dt1)
        {
            return rep.getCategoryByMonthStatistic(dt1);
        }
        public Dictionary<string, float> getStoresByDayStatistic(DateTime dt1)
        {
            return rep.getStoresByDayStatistic(dt1);
        }
        public Dictionary<string, float> getStoresBy2DatesStatistic(DateTime dt1,DateTime dt2)
        {
            return rep.getStoresBy2DaysStatistic(dt1,dt2);
        }
        public Dictionary<string, float> getStoresByMonthStatistic(DateTime dt1)
        {
            return rep.getStoresByMonthStatistic(dt1);
        }
        public Dictionary<string,float> getCostByDayStatistic(DateTime dt1)
        {
            return rep.getCostByDayStatistic( dt1);
        }
        public Dictionary<string, float> getCostBy2DatesStatistic(DateTime dt1,DateTime dt2)
        {
            return rep.getCostBy2DaysStatistic(dt1,dt2);
        }

        public void connectSqlServer()
        {
            ScannedProduct s= rep.connectSqlServer();
        }

        public Dictionary<string,float> getCostByMonthStatistic(DateTime dt)
        {
            return rep.getCostByMonthStatistic(dt);
        }
    }
}
