using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product : INotifyPropertyChanged
    {
        private Category _category;
        private Delegate[] InvocationList;
        private int _productID;
        private string _name;
        private string _imageUrl;

        public Product( string name, string url,Category category)
        {

            
            this._name = name;
            this._imageUrl = url;
            this._category = category;
        }



        public Category category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("category");
            }
        }

        public int productId
        {
            get { return _productID; }
            set
            {
                _productID = value;
                OnPropertyChanged("productId");
            }
        }
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }
        public string imageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("imageUrl");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        public void ClearPropertyChanged() { InvocationList = PropertyChanged.GetInvocationList(); PropertyChanged = null; }
        public void RestorePropertyChanged()
        {
            foreach (var item in InvocationList)
            {
                PropertyChanged += (PropertyChangedEventHandler)item;
            }
        }


    }
}
