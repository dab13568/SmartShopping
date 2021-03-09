using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BE;
using LiveCharts;
using LiveCharts.Wpf;

namespace SmartShopping
{
    /// <summary>
    /// Interaction logic for testwindow.xaml
    /// </summary>
    public partial class testwindow : Window
    {
        private Dictionary<Product, double> _myDict;
        public Dictionary<Product, double> myDict
        {
            get { return _myDict; }
            set { _myDict = value; }
        }

        public testwindow()
        {
            InitializeComponent();
            
            DataContext = this;

             myDict = new Dictionary<Product, double>
        {
            { new Product(123,"אשכולית","pack://application:,,,/Images/defaultImg.png",Category.שתיה), 0.2 },
            { new Product(123,"תפוז","pack://application:,,,/Images/defaultImg.png",Category.שתיה), 0.54 }        };

        }



    }

}
