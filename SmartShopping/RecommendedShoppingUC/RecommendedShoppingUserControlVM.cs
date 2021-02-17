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

        private List<Product> _PurchaseHistoryList;
        public List<Product> PurchaseHistoryList
        {
            get { return _PurchaseHistoryList; }
            set
            {
                _PurchaseHistoryList = value;
                OnPropertyChanged("ProductsList");
            }
        }

        public RecommendedShoppingUserControlV View;

        public RecommendedShoppingUserControlVM(RecommendedShoppingUserControlV view)
        {
            this.View = view;
            PurchaseHistoryList = new RecommendedShoppingUserControlM().GetRecommendedShoppingList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
