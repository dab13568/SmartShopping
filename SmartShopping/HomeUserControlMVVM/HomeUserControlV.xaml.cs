using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System;
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

namespace SmartShopping.HomeUserControlMVVM
{
    /// <summary>
    /// Interaction logic for HelloUCV.xaml
    /// </summary>
    public partial class HomeUserControlV : UserControl
    {
        HomeUserControlVM VM;
        public HomeUserControlV()
        {
            InitializeComponent();
            VM= new HomeUserControlVM(this);
            DataContext = VM;
        }

        private void UrlButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(VM.InfUrl);         
        }
    }
}