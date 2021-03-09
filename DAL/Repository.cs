using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
                    product.imageUrl = "pack://application:,,,/Images/defaultImg.png";
                context.products.Add(product);
                context.SaveChanges();
            } 
        }

        public void add_ScannedProduct(ScannedProduct scan)
        {
            using (var context = new ProductDB())
            {

                var arr = (from p in context.products where p.num == scan.productNo select p).ToList<Product>();
                if (arr.Count == 0)
                {
                    add_Product(new Product(scan.productNo, "", "", null));

                }
                var temp = context.scans.FirstOrDefault(value => value.productNo == scan.productNo && value.store.Equals(scan.store) && (DbFunctions.TruncateTime(scan.dateScan) == (DbFunctions.TruncateTime(value.dateScan))));
                if (temp != null)
                {
                    temp.amount++;
                    context.SaveChanges();

                    // update_ScannedProduct(temp);
                }
                if (temp == null)
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
        
        public Dictionary<string,float> getStatisticData(int subject,int timePeriod,DateTime? dt1, DateTime? dt2)
        {
            Dictionary<string, float> dict=new Dictionary<string, float>();
            switch(subject)
            {
                case 0: switch(timePeriod)
                    {
                        case 0:
                            dict= getProductsByDayStatistic(dt1); break;
                        case 2:
                            dict= getProductsBy2DaysStatistic(dt1, dt2);break;
                        case 1:
                            dict = getProductsByMonthStatistic(dt1); break;
                        
                    } break;
                case 1:
                    switch (timePeriod)
                    {
                        case 0:
                            dict = getCategoryByDayStatistic(dt1); break;
                        case 2:
                            dict = getCategoryBy2DaysStatistic(dt1, dt2); break;
                        case 1:
                            dict = getCategoryByMonthStatistic(dt1 ?? DateTime.Now); break;

                    }
                    break;
                case 2:
                    switch (timePeriod)
                    {
                        case 0:
                            dict = getStoresByDayStatistic(dt1 ?? DateTime.Now); break;
                        case 2:
                            dict = getStoresBy2DaysStatistic(dt1 ?? DateTime.Now, dt2 ?? DateTime.Now); break;
                        case 1:
                            dict = getStoresByMonthStatistic(dt1 ?? DateTime.Now); break;

                    }
                    break;
                case 3:
                    switch (timePeriod)
                    {
                        case 0:
                            dict = getCostByDayStatistic(dt1 ?? DateTime.Now); break;
                        case 2:
                            dict = getCostBy2DaysStatistic(dt1 ?? DateTime.Now, dt2 ?? DateTime.Now); break;
                        case 1:
                            dict = getCostByMonthStatistic(dt1 ?? DateTime.Now); break;

                    }
                    break;
                    
            }
            return dict;
        }

        public List<(float, float)> getCouplesOfProductForProductRecommendation()
        {
            List<(float, float)> result = new List<(float, float)>();
            List<ScannedProduct> dbList;
            using (var db = new ProductDB())
            {
                dbList = db.scans.ToList<ScannedProduct>();
                for(int i=0; i< dbList.Count()-1;i++)
                {
                    for(int j=i+1;j<= dbList.Count()-1;j++)
                    {
                        if(Math.Abs(dbList[i].dateScan.Subtract(dbList[j].dateScan).TotalHours)<=3 && dbList[i].productNo!=dbList[j].productNo)
                        {
                            result.Add((dbList[i].productNo, dbList[j].productNo));
                        }

                    }
                }
            }
            return result;
        }

        public Dictionary<DateTime,List<int>> getAllpurchasesIds(DateTime dt1)
        {
            Dictionary<DateTime, List<int>> res = new Dictionary<DateTime, List<int>>();
            List<ScannedProduct> tempScans = new List<ScannedProduct>();


            using (var db = new ProductDB())
            {
                tempScans = db.scans.ToList();
                for (int i = 0; i < tempScans.Count() ; i++)
                {
                    if (tempScans[i].dateScan.DayOfWeek == dt1.DayOfWeek)
                    {
                        if (!res.ContainsKey(tempScans[i].dateScan))
                            res[tempScans[i].dateScan] = new List<int>();
                        res[tempScans[i].dateScan].Add(tempScans[i].productNo);
                    }
                }
            }
            return res;
        }

        public List<(int, int)> getCouplesOfProductForDayRecommendation(DateTime dt1)
        {
            List<(int, int)> result = new List<(int, int)>();

            using (var db = new ProductDB())
            {
                for (int i = 0; i < db.scans.Count() - 1; i++)
                {
                    if (DbFunctions.TruncateTime(db.scans.Skip(i).First().dateScan) == DbFunctions.TruncateTime(dt1))
                    {
                        for (int j = i + 1; j <= db.scans.Count() - 1; j++)
                        {
                            if (Math.Abs(db.scans.Skip(i).First().dateScan.Subtract(db.scans.Skip(j).First().dateScan).TotalHours) <= 1)
                            {
                                result.Add((db.scans.Skip(i).First().productNo, db.scans.Skip(j).First().productNo));
                            }

                        }
                    }
                }
            }
            return result;

        }
        public ScannedProduct connectSqlServer()
        {
            ScannedProduct res = new ScannedProduct() ;
            using (var db = new ProductDB())
            {
                if(db.scans!=null)
                res= db.scans.FirstOrDefault(value=> value.Id>=0);
            }
            return res;
        }

        public ObservableCollection<ScannedProduct> getCurrentDayScannedProducts(DateTime dt)
        {
            List<ScannedProduct> result;
            //value.dateScan.ToShortDateString().Equals(dt.ToShortDateString())
            using (var context = new ProductDB())
            {
                result = (from p in context.scans where DbFunctions.TruncateTime(p.dateScan) == DbFunctions.TruncateTime(dt) select p).ToList<ScannedProduct>();
            }
            return new ObservableCollection<ScannedProduct>(result);
        }
        public ObservableCollection<ScannedProduct> getScannedProductBetween2Days(DateTime dt1, DateTime dt2)
        {
            List<ScannedProduct> result;
            //value.dateScan.ToShortDateString().Equals(dt.ToShortDateString())
            using (var context = new ProductDB())
            {
                result = (from p in context.scans where DbFunctions.TruncateTime(p.dateScan) >= DbFunctions.TruncateTime(dt1) && DbFunctions.TruncateTime(p.dateScan) <= DbFunctions.TruncateTime(dt2)  select p).ToList<ScannedProduct>();
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

        public Dictionary<string, float> getProductsByDayStatistic(DateTime? dt)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            //string name = "";
            using (var context = new ProductDB())
            {
                foreach (var productScan in (from p in context.scans where DbFunctions.TruncateTime(p.dateScan)== DbFunctions.TruncateTime(dt) select p).ToList<ScannedProduct>())
                {
                    var product = context.products.FirstOrDefault(value => value.num ==productScan.productNo );
                    if (!dict.ContainsKey(product.name))
                        dict[product.name] = 0;
                    dict[product.name] += productScan.amount;
                }
            }
            return dict;
        }

        public Dictionary<string, float> getCategoryByDayStatistic(DateTime? dt)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            //string name = "";
            using (var context = new ProductDB())
            {
                foreach (var productScan in (from p in context.scans where DbFunctions.TruncateTime(p.dateScan) == DbFunctions.TruncateTime(dt) select p).ToList<ScannedProduct>())
                {
                    var product= context.products.FirstOrDefault(value => value.num == productScan.productNo);
                    if (!dict.ContainsKey(product.category.ToString()))
                        dict[product.category.ToString()] = 0;
                    dict[product.category.ToString()]+=productScan.amount;
                }
            }
            return dict;
        }

        public Dictionary<string, float> getStoresByDayStatistic(DateTime dt)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            using (var context = new ProductDB())
            {
                foreach (var product in (from p in context.scans where DbFunctions.TruncateTime(p.dateScan) == DbFunctions.TruncateTime(dt) select p).ToList<ScannedProduct>())
                {
                    //name = context.products.FirstOrDefault(value => value.num == productNum)..ToString();
                    if (!dict.ContainsKey(product.store))
                        dict[product.store] = 0;
                    dict[product.store]+=product.amount;
                }
            }
            return dict;
        }

        //public Dictionary<string,float> getCostByDayStatistic()
        //{
        //    Dictionary<string, float> dict = new Dictionary<string, float>();
        //    using (var context = new ProductDB())
        //    {

        //        foreach (var product in context.scans)
        //        {
        //            if (!dict.ContainsKey(product.dateScan.DayOfWeek.ToString()))
        //                dict[product.dateScan.DayOfWeek.ToString()] = 0;
        //            dict[product.dateScan.DayOfWeek.ToString()] += product.cost * product.amount;
        //        }
        //    }
        //    return dict;
        //}
        public Dictionary<string, float> getCostByDayStatistic(DateTime dt)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            using (var context = new ProductDB())
            {

                foreach (var product in context.scans)
                {
                    if (product.dateScan.Date== dt.Date)
                    {
                        if (!dict.ContainsKey(new System.Globalization.CultureInfo("he-IL").DateTimeFormat.GetDayName(product.dateScan.DayOfWeek)+","+ dt.ToShortDateString()))
                            dict[new System.Globalization.CultureInfo("he-IL").DateTimeFormat.GetDayName(product.dateScan.DayOfWeek) + "," + dt.ToShortDateString()] = 0;
                        dict[new System.Globalization.CultureInfo("he-IL").DateTimeFormat.GetDayName(product.dateScan.DayOfWeek) + "," + dt.ToShortDateString()] += product.cost * product.amount;
                    }
                }
            }
            return dict;
        }

        public Dictionary<string, float> getProductsBy2DaysStatistic(DateTime? dt1, DateTime? dt2)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            //string name ;
            
            using (var context = new ProductDB())
            {
                foreach (var productScan in getScannedProductBetween2Days(dt1 ?? DateTime.Now, dt2 ?? DateTime.Now))
                {
                    
                    var product = context.products.FirstOrDefault(value => value.num == productScan.productNo);

                    if (!dict.ContainsKey(product.name))
                        dict[product.name] = 0;
                    dict[product.name]+=productScan.amount;
                }
            }
            return dict;
        }

        public Dictionary<string, float> getCategoryBy2DaysStatistic(DateTime? dt1, DateTime? dt2)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            //string name;

            using (var context = new ProductDB())
            {
                foreach (var productScan in getScannedProductBetween2Days(dt1 ?? DateTime.Now, dt2 ?? DateTime.Now))
                {

                    var product = context.products.FirstOrDefault(value => value.num == productScan.productNo);

                    if (!dict.ContainsKey(product.category.ToString()))
                        dict[product.category.ToString()] = 0;
                    dict[product.category.ToString()]+= productScan.amount;
                }
            }
            return dict;
        }

        public Dictionary<string, float> getStoresBy2DaysStatistic(DateTime dt1, DateTime dt2)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();

            using (var context = new ProductDB())
            {
                foreach (var product in getScannedProductBetween2Days(dt1, dt2))
                {

                    //name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                    if (!dict.ContainsKey(product.store))
                        dict[product.store] = 0;
                    dict[product.store]+=product.amount;
                }
            }
            return dict;
        }


        public Dictionary<string,float> getCostBy2DaysStatistic(DateTime dt1, DateTime dt2)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            double period = Math.Floor((dt2-dt1).TotalDays/6);
            
            using (var context = new ProductDB())
            {
                foreach (var product in getScannedProductBetween2Days(dt1, dt2))
                {
                    DateTime fixedDt = dt1.AddDays(Math.Ceiling(Math.Ceiling((product.dateScan - dt1).TotalDays / 6) * period));
                    if(fixedDt>dt2)
                    {
                        if (!dict.ContainsKey(dt2.ToShortDateString()))
                            dict[dt2.ToShortDateString()] = 0;
                        dict[dt2.ToShortDateString()] += product.cost * product.amount;
                    }
                    else
                    {
                        if (!dict.ContainsKey(fixedDt.ToShortDateString()))
                            dict[fixedDt.ToShortDateString()] = 0;
                        dict[fixedDt.ToShortDateString()] += product.cost * product.amount;
                    }
                    //name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                }
            }
            return dict;
        }


        public Dictionary<string, float> getProductsByMonthStatistic(DateTime? dt)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();

            using (var context = new ProductDB())
            {
                
                foreach (var productScan in (from p in context.scans where p.dateScan.Month == ((DateTime)dt).Month select p).ToList<ScannedProduct>())
                {
                    var product = context.products.FirstOrDefault(value => value.num == productScan.productNo);
                    if (!dict.ContainsKey(product.name))
                        dict[product.name] = 0;
                    dict[product.name]+=productScan.amount;
                }
            }
            return dict;
        }
        public Dictionary<string, float> getCategoryByMonthStatistic(DateTime dt)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();

            using (var context = new ProductDB())
            {
                foreach (var productScan in (from p in context.scans where p.dateScan.Month == dt.Month select p).ToList<ScannedProduct>())
                {
                    
                    var product = context.products.FirstOrDefault(value => value.num == productScan.productNo);

                    if (!dict.ContainsKey(product.category.ToString()))
                        dict[product.category.ToString()] = 0;
                    dict[product.category.ToString()] +=productScan.amount;
                }
            }
            return dict;
        }

        public Dictionary<string, float> getStoresByMonthStatistic(DateTime dt)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();

            using (var context = new ProductDB())
            {
                foreach (var product in (from p in context.scans where p.dateScan.Month == dt.Month select p).ToList<ScannedProduct>())
                {

                    //name = context.products.FirstOrDefault(value => value.num == product.productNo).name;

                    if (!dict.ContainsKey(product.store))
                        dict[product.store] = 0;
                    dict[product.store]+=product.amount;
                }
            }
            return dict;
        }

        //public Dictionary<string, float> getCostByMonthStatistic(DateTime dt)
        //{
        //    Dictionary<string, float> dict = new Dictionary<string, float>();

            

        //    using (var context = new ProductDB())
        //    {
        //        foreach (var product in context.scans)
        //        {
        //            if (product.dateScan.Month == dt.Month && product.dateScan.Year == dt.Year)
        //            {
        //                if (!dict.ContainsKey(new System.Globalization.CultureInfo("he-IL").DateTimeFormat.GetDayName(product.dateScan.DayOfWeek)))
        //                    dict[new System.Globalization.CultureInfo("he-IL").DateTimeFormat.GetDayName(product.dateScan.DayOfWeek)] = 0;
        //                dict[new System.Globalization.CultureInfo("he-IL").DateTimeFormat.GetDayName(product.dateScan.DayOfWeek)] += product.cost * product.amount;
        //            }
        //        }
        //    }
        //    return dict;
        //}


        public Dictionary<string, float> getCostByMonthStatistic(DateTime dt)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();



            using (var context = new ProductDB())
            {
                foreach (var product in context.scans)
                {
                    if (product.dateScan.Month == dt.Month && product.dateScan.Year == dt.Year)
                    {
                        if (!dict.ContainsKey("week" +(((product.dateScan.Day-1)/7)+1)))
                            dict["week" + (((product.dateScan.Day - 1) / 7) + 1)] = 0;
                        dict["week" + (((product.dateScan.Day - 1) / 7) + 1)] += product.cost * product.amount;
                    }
                }
            }
            return dict;
        }
        public Product getProductById(int id)
        {
            Product res = new Product();
            using (var context = new ProductDB())
            {
                res=context.products.FirstOrDefault(value => value.num == id);
            }
            return res;
        }

        //public Dictionary<string,float> getCostByMonthStatistic()
        //{
        //    Dictionary<string, float> dict = new Dictionary<string, float>();



        //    using (var context = new ProductDB())
        //    {
        //        foreach (var product in context.scans)
        //        {

        //            if (!dict.ContainsKey(product.dateScan.ToString("MMMM")))
        //                dict[product.dateScan.ToString("MMMM")] = 0;
        //            dict[product.dateScan.ToString("MMMM")] += product.cost * product.amount;
        //        }
        //    }
        //    return dict;
        //}

    }
}