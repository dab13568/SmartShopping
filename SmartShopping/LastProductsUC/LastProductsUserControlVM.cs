using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartShopping.LastProductsUC
{
    public class LastProductsUserControlVM : INotifyPropertyChanged
    {
        private List<Product> _SourceList;
        public List<Product> SourceList
        {
            get { return _SourceList; }
            set
            {
                _SourceList = value;
                OnPropertyChanged("SourceList");
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private Brush _messageColor;
        private Brush MessageColor
        {
            get { return _messageColor; }
            set
            {
                _messageColor = value;
                OnPropertyChanged("MessageColor");
            }
        }

        public LastProductsUserControlV View;

        public LastProductsUserControlVM(LastProductsUserControlV view, List<Product> products)
        {
            this.View = view;
            SourceList = products;

        }

        public LastProductsUserControlVM(LastProductsUserControlV view)
        {
            this.View = view;
            SourceList = new LastProductsUserControlM().GetLastProducts();
        }

      

        // INotifyPropertyChanged implementaion
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
