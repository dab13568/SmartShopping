using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartShopping.MainWindowMVVM
{
    class MenuCMD : ICommand
    {
        private MainWindowVM VM;

        public MenuCMD(MainWindowVM VM)
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
            if (parameter != null)
            {
                try
                {
                    switch ((parameter))
                    {
                        case 0:
                            VM.HomeView.Execute(parameter);
                            break;
                        case 1:
                            VM.LastProductsView.Execute(parameter);
                            break;
                        default:
                            VM.HomeView.Execute(parameter);
                            break;
                    }
                
                }
                catch(Exception e)
                { }
            }
        }
    }
}
