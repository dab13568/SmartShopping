using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product : INotifyPropertyChanged
    {
        public int productID { get; set; }
        private int _num;
        private Category? _category;
        private Delegate[] InvocationList;
        private string _name;
        private string _imageUrl;

        public Product(int num, string name, string url, Category? category)
        {
            //productID = id;
            this._num = num;
            this._name = name;
            this._imageUrl = url;
            this._category = category;
        }

        public Product()
        {
        }

        public Category? category
        {
            get { return _category; }
            set
            {
                if (value is int)
                    _category = (Category)value;
                else
                    _category = value;
                OnPropertyChanged("category");
            }
        }

        public int num
        {
            get { return _num; }
            set
            {
                _num = value;
                OnPropertyChanged("num");
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



        public override string ToString()
        {
            return name;
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