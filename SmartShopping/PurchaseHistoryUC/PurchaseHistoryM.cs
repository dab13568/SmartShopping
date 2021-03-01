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

        internal ObservableCollection<ScannedProduct> GetPurchaseHistoryList()
        {
            return new BLimp().Get_all_ScannedProducts(); ;
        }
    }
}