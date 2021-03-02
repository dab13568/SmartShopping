using BE;
using BL;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.LastProductsUC
{
    public class LastProductsUserControlM
    {
        public ObservableCollection<ScannedProduct> GetLastProducts()
        {
            return new BLimp().getCurrentDayScannedProducts();
        }

        internal void SaveChanges(Product product)
        {
            try
            {
                product.ClearPropertyChanged();
                BLimp bl = new BLimp();
                bl.update_Product(product);
                product.RestorePropertyChanged();
            }
            catch (Exception)
            {
                throw new Exception("לא ניתן לבצע שינויים כרגע");
            }
        }

    }
}