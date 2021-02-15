using BE;
using System;
using System.Collections.Generic;

namespace SmartShopping.PurchaseHistoryUC
{
    internal class PurchaseHistoryM
    {
        public PurchaseHistoryM()
        {
        }

        internal List<Product> GetPurchaseHistoryList()
        {
            List<Product> l = new List<Product> { new Product(), new Product() };
            l[0].name = "דוגמא של סתם טקסט";
            l[1].name = "דוגמא של סתם טקסט";

            return l;
        }
    }
}