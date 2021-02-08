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
        private string imageUrl;
        private float cost;
        private int rating;

    }
}
