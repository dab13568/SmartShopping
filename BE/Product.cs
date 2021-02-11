using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product
    {
        public enum Category
        {
            food,
            drinks,
            hygene
        }

        public int ID;
        public string _name;
        public string _imageUrl;
        public float _cost;
        public int _rating;
        public Store _store;
        public DateTime _dateScan;
        public Product()
        {

        }
    }
}
