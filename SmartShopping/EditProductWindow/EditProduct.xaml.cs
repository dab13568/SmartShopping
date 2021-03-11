using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartShopping.EditProductWindow
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        ScannedProduct _sp;
        EditProductVM VM;
        public EditProduct( ref ScannedProduct  sp)
        {
            _sp = sp;
            InitializeComponent();
            VM= new EditProductVM(this, ref _sp);
            this.DataContext = VM;
        }

        public void loadDialog(Microsoft.Win32.OpenFileDialog dlg)
        {
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                VM.p.imageUrl = dlg.FileName;
            }
        }
       
        void closingWindow(object sender, CancelEventArgs e)
        {
            VM.closingWindow();
        }
    }
}
