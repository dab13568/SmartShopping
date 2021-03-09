using BL;
using SmartShopping.EditProductWindow;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartShopping.PurchaseHistoryUC
{
    /// <summary>
    /// Interaction logic for PurchaseHistoryV.xaml
    /// </summary>
    public partial class PurchaseHistoryV : UserControl
    {
        public PurchaseHistoryV()
        {
            InitializeComponent();
            DataContext = new PurchaseHistoryVM(this);
        }

        public void loadEditProductView(EditProduct ep)
        {
            ep.ShowDialog();
        }
    }
}
