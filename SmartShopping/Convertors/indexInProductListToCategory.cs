using BL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SmartShopping.Convertors
{
    public class indexInProductListToCategory : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string cat = new BLimp().getCategoryStringByProductId((int)value);
            if (cat != null && cat != "")
                return "היא "+cat ;
            return "חסרה במערכת. ";
        }
    

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
