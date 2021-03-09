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

        public ObservableCollection<Product> GetProductList()
        {
            return new BLimp().Get_all_Products();
        }

        public Dictionary<Product, float> getProbabilityDidt(int id)
        {
            return new BLimp().getProbabilityDidt(id);
        }
    }
}
