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
    public class indexInProductListToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string img= new BLimp().getImageUrlByProductId((int)value);
            if (img != null && img != "")
                return img;
            return "pack://application:,,,/Images/defaultImg.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
