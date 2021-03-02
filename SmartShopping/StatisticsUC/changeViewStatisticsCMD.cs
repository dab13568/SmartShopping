using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartShopping.StatisticsUC
{
    class changeViewStatisticsCMD : ICommand
    {

        private StatisticsUserControlVM VM;

        public changeViewStatisticsCMD()
        {
        }

        public changeViewStatisticsCMD(StatisticsUserControlVM VM)
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
            VM.changeView();
        }
    }
}
