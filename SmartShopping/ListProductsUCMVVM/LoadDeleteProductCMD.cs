using BE;
using BL;
using SmartShopping.LastProductsUC;
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
        private LastProductsUserControlVM VM;

        public LoadDeleteProductCMD(LastProductsUserControlVM VM)
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
            ScannedProduct s = (ScannedProduct)parameter;
            BLimp bl = new BLimp();
            bl.delete_ScannedProduct(s);
            VM.worker.RunWorkerAsync();
        }
    }
}
