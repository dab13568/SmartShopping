using BE;
using BL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.LastProductsUC
{
    public class LastProductsUserControlM
    {
        public List<Product> GetLastProducts()
        {
            return new BLimp().GetLastProducts();
        }

        internal void SaveChanges(Product product)
        {
            try
            {
                product.ClearPropertyChanged();
                BLimp bl = new BLimp();
                //string parms = bl.TranslateHEtoEN(product.GetParms());
                //product.NutritionalValues = bl.GetProductIdNutrition(parms);
                //product.NutritinosValuesDictonary = bl.GetProductNutritionByID(product.NutritionalValues);
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
