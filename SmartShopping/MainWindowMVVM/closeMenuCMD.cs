using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SmartShopping.MainWindowMVVM
{
    class closeMenuCMD : ICommand
    {
        private MainWindowVM VM;

        public closeMenuCMD(MainWindowVM VM)
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
            VM.VisibilityCloseMenu = Visibility.Collapsed;
            VM.VisibilityOpenMenu = Visibility.Visible;
        }
    }
}
