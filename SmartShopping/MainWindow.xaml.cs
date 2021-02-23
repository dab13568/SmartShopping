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
using SmartShopping.MainWindowMVVM;
using SmartShopping.HomeUserControlMVVM;
using SmartShopping.LastProductsUC;
using System.Windows.Media.Animation;
using SmartShopping.PurchaseHistoryUC;
using BE;
using SmartShopping.StatisticsUC;
using System.Threading;
using System.ComponentModel;
using SmartShopping.RecommendedShoppingUC;
using System.IO;

namespace SmartShopping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserControl CurrnetUserControl
        {
            get { return _currnetUserControl; }
            set
            {
                GridLoadUC.Children.Remove(CurrnetUserControl);
                _currnetUserControl = value;
                _currnetUserControl.HorizontalAlignment = HorizontalAlignment.Stretch;
                _currnetUserControl.VerticalAlignment = VerticalAlignment.Stretch;
                GridLoadUC.Children.Add(_currnetUserControl);
                //this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Images/background.jpg")));
            }
        }
        private UserControl _currnetUserControl;

        PurchaseHistoryV PH;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowVM(this);
            LoadHomeView();

            //DAL.GoogleDriveApi drive = new DAL.GoogleDriveApi();
            drive.Connect();

            //MessageBox.Show(reader.Decode(@"C:\courses\SmartShopping\SmartShopping\SmartShopping\Images\Barcodes\qr_example.png"));


            DAL.Barcode_reader reader = new DAL.Barcode_reader();
            string[] files = Directory.GetFiles(@"C:\courses\SmartShopping\SmartShopping\SmartShopping\Images\Barcodes\");
            foreach (var file in files)
            {
               
                //MessageBox.Show(file.Substring(69,file.IndexOf("at")-69));
                DateTime dateTime = DateTime.Parse(file.Substring(69, file.IndexOf("at") - 69));
                MessageBox.Show(dateTime.Date.ToString());
                BL.BLimp bLimp = new BL.BLimp();
                String text = reader.Decode(file);
                
                string[] str = text.Split(',');

                bLimp.add_ScannedProduct(new ScannedProduct(Int32.Parse(str[0]), str[1], dateTime, Int32.Parse(str[2]), Int32.Parse(str[3])));
                // using the method 
               

                //MessageBox.Show(file.IndexOf("at").ToString());
                //MessageBox.Show(reader.Decode(file));
            }



            //DAL.Repository rep = new DAL.Repository();

            //new Product(9, "shlomichai", @"kuku.url",Category.clothes);
            //3,new Store(4,"yami",new Address("8","Meshorer",4,"PT")),DateTime.Now,10,10
            //foreach (var p in rep.Get_all_Products())
            //{
            //    MessageBox.Show(p.ToString());
            //}
        }



        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            DAL.Repository rep = new DAL.Repository();
            foreach (var s in rep.Get_all_Products())
            {
                MessageBox.Show(s.ToString());
            }
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;

        }

        internal void LoadHomeView()
        {
            HomeUserControlV Home = new HomeUserControlV();
            CurrnetUserControl = Home;
            closeMenuStoryBoard();
        }

        internal void LoadLastProductsView()
        {
            LastProductsUserControlV LP = new LastProductsUserControlV();
            CurrnetUserControl = LP;
            closeMenuStoryBoard();
        }

        internal void LoadPurchaseHistoryView()
        {
            PurchaseHistoryV PH = new PurchaseHistoryV();
            CurrnetUserControl = PH;
            closeMenuStoryBoard();
        }

        internal void LoadStatisticsView()
        {
            StatisticsUserControlV S = new StatisticsUserControlV();
            CurrnetUserControl = S;
            closeMenuStoryBoard();
        }


        internal void LoadRecommendedShoppingView()
        {
            RecommendedShoppingUserControlV RS = new RecommendedShoppingUserControlV();
            CurrnetUserControl = RS;
            closeMenuStoryBoard();
        }
        public void closeMenuStoryBoard()
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
            sb.Begin();
        }

      
    }


}
    