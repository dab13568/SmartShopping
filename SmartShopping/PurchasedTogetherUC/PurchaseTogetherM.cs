﻿using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.PurchasedTogetherUC
{
    internal class PurchaseTogetherM
    {
        public PurchaseTogetherM()
        {

        }

        internal List<Product> GetPurchaseTogetherList()
        {
            List<Product> l = new List<Product> { new Product(), new Product(), new Product(), new Product(), new Product(), new Product(), new Product(), new Product(), new Product(), new Product(), new Product(), new Product(), new Product() };
            l[0].name = l[1].name = l[2].name = l[3].name = l[4].name = l[5].name = l[6].name = l[7].name = l[8].name = l[9].name = l[10].name = l[11].name = l[12].name = "דוגמא של סתם טקסט";
            l[0].imageUrl = "../Images/bamba.jpg";
            l[1].imageUrl = "../Images/bisli.jpg";
            l[2].imageUrl = "../Images/cake.jpg";
            l[3].imageUrl = "../Images/carrot.jpg";
            l[4].imageUrl = "../Images/dordorant.jpeg";
            l[5].imageUrl = "../Images/pasta.jpg";
            l[6].imageUrl = "../Images/carrot.jpg";
            l[7].imageUrl = "../Images/dordorant.jpeg";
            l[8].imageUrl = "../Images/pasta.jpg";
            l[9].imageUrl = "../Images/bamba.jpg";
            l[10].imageUrl = "../Images/bisli.jpg";
            l[11].imageUrl = "../Images/cake.jpg";
            l[12].imageUrl = "../Images/carrot.jpg";

            return l;
        }
    }
}
