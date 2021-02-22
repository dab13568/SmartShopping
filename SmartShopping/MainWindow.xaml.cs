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
using DAL;
using SmartShopping.PurchasedTogetherUC;

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
            //DAL.Repository rep = new DAL.Repository();
            //new ScannedProduct(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.food);
            //new Product(9, "cola", @"kuku.url", Category.drinks);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);
            //new Product(9, "Bamba", @"kuku.url", Category.clothes);

            //3,new Store(4,"yami",new Address("8","Meshorer",4,"PT")),DateTime.Now,10,10
            //rep.add_Product(new Product(9, "shlomichai", @"kuku.url", Category.clothes));
            //googleDriveConnection drive = new googleDriveConnection();

        }



        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            //DAL.Repository rep = new DAL.Repository();
            //foreach (var s in rep.Get_all_Products())
            //{
            //    MessageBox.Show(s.ToString());
            //}
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

        internal void LoadPurchaseTogetherView()
        {
            PurchasedTogetherUserControlV PT = new PurchasedTogetherUserControlV();
            CurrnetUserControl = PT;
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
    