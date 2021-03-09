﻿using BE;
using BL;
using SmartShopping.EditProductWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartShopping.ListProductsUCMVVM
{
    class LoadEditProductCMD : ICommand
    {
        private ILoadEditWindow VM;

        public LoadEditProductCMD(ILoadEditWindow VM)
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
            EditProduct ep = new EditProduct(ref s);
            VM.LoadEditProductView(ep);
        }
    }
}
