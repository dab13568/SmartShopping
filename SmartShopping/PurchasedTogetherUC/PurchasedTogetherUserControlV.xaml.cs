﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartShopping.PurchasedTogetherUC
{
    /// <summary>
    /// Interaction logic for PurchasedTogetherUserControlV.xaml
    /// </summary>
    public partial class PurchasedTogetherUserControlV : UserControl
    {
        public PurchasedTogetherUserControlV()
        {
            InitializeComponent();
            DataContext = new PurchasedTogetherVM(this);
        }
    }
}
