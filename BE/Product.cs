using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product: INotifyPropertyChanged
    {
        public enum Category
        {
            food,
            drinks,
            hygene
        }

        public int ProductID;
        public string name;
        public string _imageUrl;
        public float _cost;


        public Product()
        {

        }

        private Delegate[] InvocationList;
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
