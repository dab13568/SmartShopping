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
using System.Windows.Shapes;

namespace SmartShopping
{
    /// <summary>
    /// Interaction logic for testwindow.xaml
    /// </summary>
    public partial class testwindow : Window
    {
        public testwindow()
        {
            InitializeComponent();
            //var image = new Image();
            //var fullFilePath = @"http://drive.google.com/uc?export=view&id=1o7dRUMWsmpHvQxjELjCmJQ_tRg0wL7iA";
            //DAL.googleDriveConnection drive = new DAL.googleDriveConnection();
            //testTB.Text = drive.Initiallize();
            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            //bitmap.EndInit();

            //image.Source = bitmap;
            //testGrid.Children.Add(image);
            DAL.GoogleDriveApi drive = new DAL.GoogleDriveApi();
            drive.Connect();
        }
    }
}
