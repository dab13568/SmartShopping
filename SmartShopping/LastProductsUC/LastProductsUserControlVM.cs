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
        private List<Product> _LastproductsList;
        public List<Product> LastproductsList
        {
            get { return _LastproductsList; }
            set
            {
                _LastproductsList = value;
                OnPropertyChanged("ProductsList");
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
            Initialize(products);

        }

        public LastProductsUserControlVM(LastProductsUserControlV view)
        {
            this.View = view;
            Initialize(new LastProductsUserControlM().GetLastProducts());
        }

        private void Initialize(List<Product> products)
        {
            //Message = "לעריכת מוצר לחץ פעמיים על המאפיין אותו תרצה לערוך";
            //MessageColor = Brushes.Black;
            LastproductsList = products;
            foreach (var item in LastproductsList)
            {
                item.PropertyChanged += (x, y) => { SaveChanges(item); };
            }
        }

        private void SaveChanges(Product product)
        {
            new Thread(() =>
            {
                Message = "שומר שינויים...";
                try
                {
                    new LastProductsUserControlM().SaveChanges(product);
                    Message = "השינויים נשמרו";
                    MessageColor = Brushes.Black;
                }
                catch (Exception ex)
                {
                    Message = ex.Message + ", השינויים שביצעת לא נשמרו";
                    MessageColor = Brushes.Red;
                }
            }).Start();
        }



        // INotifyPropertyChanged implementaion
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
