using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Product
    {
        private enum Category
        {
            food,
            drinks,
            hygene
        }
        private int _id;
        private string _name;
        private string _imageUrl;
        private float _cost;
        private int _rating;
        private Store _store;
    }
}
