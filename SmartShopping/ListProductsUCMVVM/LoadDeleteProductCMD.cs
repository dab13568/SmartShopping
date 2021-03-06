using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartShopping.ListProductsUCMVVM
{
    class LoadDeleteProductCMD : ICommand
    {
        private ListProductsVM VM;

        public LoadDeleteProductCMD(ListProductsVM VM)
        {
            this.VM = VM;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;

        }

        public void Execute(object parameter)
        {
            BLimp bl = new BLimp();
            bl.delete_ScannedProduct(bl.Get_all_ScannedProducts()[(int)parameter]);
            VM.DeleteProduct();
        }
    }
}
