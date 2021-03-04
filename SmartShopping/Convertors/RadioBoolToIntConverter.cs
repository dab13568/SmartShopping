using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SmartShopping.Convertors
{
    class RadioBoolToIntConverter : IValueConverter
    {
        int lastclick;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int)value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                lastclick = int.Parse(parameter.ToString());
                return parameter;
            }
                
            return lastclick;
        }
    }
}
