using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.RecommendedShoppingUC
{
    class RecommendedShoppingUserControlVM : INotifyPropertyChanged
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

        public RecommendedShoppingUserControlV View;

        public RecommendedShoppingUserControlVM(RecommendedShoppingUserControlV view)
        {
            this.View = view;
            SourceList = new RecommendedShoppingUserControlM().GetRecommendedShoppingList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
