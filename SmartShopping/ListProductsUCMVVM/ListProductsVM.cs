using BE;
using BL;
using SmartShopping.EditProductWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Input;

namespace SmartShopping.ListProductsUCMVVM
{
    public class ListProductsVM
    {
        public ListProductsUC View;

        public ListProductsVM(ListProductsUC view)
        {
            this.View = view;
        }

        public ICommand EditProductView { get { return new LoadEditProductCMD(this); } }
        public ICommand DeleteProductCMD { get { return new LoadDeleteProductCMD(this); } }


        public void LoadEditProductView(int i)
        {
                                        
            ScannedProduct s = new BLimp().Get_all_ScannedProducts()[i];
            EditProduct ep = new EditProduct( ref s);
            View.loadEditProductView(ep);         

        }

        public void DeleteProduct()
        {

 
            View.ListViewProducts.ItemsSource = new BLimp().Get_all_ScannedProducts();


        }
    }
}
