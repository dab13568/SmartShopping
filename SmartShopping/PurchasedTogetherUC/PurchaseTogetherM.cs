using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.PurchasedTogetherUC
{
    internal class PurchaseTogetherM
    {
        public PurchaseTogetherM()
        {

        }

        internal ObservableCollection<Product> GetPurchaseTogetherList()
        {
            return new BLimp().Get_all_Products();
        }
    }
}
