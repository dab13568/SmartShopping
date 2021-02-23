using BE;
using BL;
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

namespace SmartShopping
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

        public void SetDataContext(ListedVM<ScannedProduct> vm)
        {
            this.DataContext = vm;
            if (vm.SourceList != null)
                foreach (var product in vm.SourceList)
                    product.PropertyChanged += (x, y) => { MessageBox.Show("Somthing changed"); };
        }

        private void editProductButton_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            int index = ListViewProducts.Items.IndexOf(button.DataContext);
            ScannedProduct s = new BLimp().Get_all_ScannedProducts()[index];
            MessageBox.Show(s.rating.ToString());
            s.rating = 4;


            DAL.Repository rep = new DAL.Repository();
            rep.update_ScannedProduct(s);


            EditProduct ep = new EditProduct(ref s);
            ep.Show();
        }
    }
}
