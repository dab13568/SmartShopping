using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartShopping.EditProductWindow
{
    public class EditProductVM: INotifyPropertyChanged
    {
        public EditProduct View;

        public Product _p;
        public Product p
        {
            get { return _p; }
            set
            {
                Product temp_p = value;
                _p = temp_p;
                OnPropertyChanged("p");
            }
        }

        public ScannedProduct _sp;
        public ScannedProduct sp
        {
            get { return _sp; }
            set
            {
                ScannedProduct s = value;
                _sp = s;
                OnPropertyChanged("sp");
            }
        }

        public ObservableCollection<int> _NumberList;
        public ObservableCollection<int> NumberList
        {
            get { return _NumberList; }
            set
            {
                ObservableCollection<int> s = value;
                _NumberList = s;
                OnPropertyChanged("NumberList");
            }
        }

        public ICommand closingWindowCommand { get { return new EditProductCMD(this); } }


        public void closingWindow()
        {
            new BLimp().update_ScannedProduct(sp);
            new BLimp().update_Product(p);
        }



        public EditProductVM(EditProduct view, ref ScannedProduct s)
        {
            this.View = view;
            this.sp = s;
            this.p = new BLimp().getProductByScannedProduct(sp);
            NumberList = new ObservableCollection<int>(Enumerable.Range(0, 1000));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

    }
}
