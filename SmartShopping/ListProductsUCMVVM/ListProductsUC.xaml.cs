using BE;
using BL;
using SmartShopping.EditProductWindow;
using SmartShopping.MainWindowMVVM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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

namespace SmartShopping.ListProductsUCMVVM
{
    /// <summary>
    /// Interaction logic for ListProductsUC.xaml
    /// </summary>
    public partial class ListProductsUC : UserControl
    {
        public ListProductsUC()
        {
            InitializeComponent();

        }

        

        public void loadEditProductView(EditProduct ep)
        {
            ep.ShowDialog();
            ListViewProducts.ItemsSource = new BLimp().Get_all_ScannedProducts();
        }

        private void editProductButton_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            int index = ListViewProducts.Items.IndexOf(button.DataContext);
            ScannedProduct s = new BLimp().Get_all_ScannedProducts()[index];
            //MessageBox.Show(s.rating.ToString());
            EditProduct ep = new EditProduct(ref s);
            ep.ShowDialog();
            ListViewProducts.ItemsSource = new BLimp().Get_all_ScannedProducts();

        }
    }
}
