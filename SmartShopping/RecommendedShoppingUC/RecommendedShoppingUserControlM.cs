using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.RecommendedShoppingUC
{
    class RecommendedShoppingUserControlM
    {
        private BLimp bl;
        public RecommendedShoppingUserControlM() 
        {
            bl = new BLimp();
        }

        internal List<Product> GetRecommendedShoppingList(string s)
        {
            return bl.getReccomendationByDay(s);
        }

        public List<string> getDaysWhichHeBought()
        {
            return bl.getDaysWhichHeBought();
        }
    }
}
