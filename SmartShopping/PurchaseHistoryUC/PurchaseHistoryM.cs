using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmartShopping.PurchaseHistoryUC
{
    internal class PurchaseHistoryM
    {
        public PurchaseHistoryM()
        {
        }

        internal ObservableCollection<ScannedProduct> GetPurchaseHistoryList(DateTime first,DateTime last)
        {
            return new BLimp().getScannedProductBetween2Days(first,last);
        }
    }
}