using BE;
using BL;
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

namespace SmartShopping
{
    /// <summary>
    /// Interaction logic for ListProductsUC.xaml
    /// </summary>
    public partial class ListProductsUC : UserControl, INotifyPropertyChanged
    {
        public ListProductsUC()
        {
            InitializeComponent();

        }

        

      

        private void editProductButton_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            int index = ListViewProducts.Items.IndexOf(button.DataContext);
            ScannedProduct s = new BLimp().Get_all_ScannedProducts()[index];

            EditProduct ep = new EditProduct(ref s);
            ep.ShowDialog();
            ListViewProducts.ItemsSource = new BLimp().Get_all_ScannedProducts();

        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
