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

    }
}