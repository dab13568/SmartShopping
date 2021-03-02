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


            DAL.Repository rep = new DAL.Repository();

            rep.add_ScannedProduct(new ScannedProduct(128, "Shufersal", DateTime.Now, 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(127, "Mega", DateTime.Now, 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(126, "Osher-Ad", DateTime.Now, 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(125, "Osher-Ad", DateTime.Now, 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(129, "Mega", DateTime.Now, 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(130, "Mega", DateTime.Now, 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(128, "Shufersal", DateTime.Now.AddDays(40), 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(128, "Shufersal", DateTime.Now.AddDays(20), 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(129, "Mega", DateTime.Now.AddDays(50), 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(129, "Mega", DateTime.Now.AddDays(20), 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(129, "Shefa", DateTime.Now.AddDays(20), 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(129, "Shefa", DateTime.Now.AddDays(90), 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(129, "Shefa", DateTime.Now.AddDays(130), 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(125, "Hiper-Mor", DateTime.Now, 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(125, "Hiper-Mor", DateTime.Now.AddDays(130), 25, 3));
            rep.add_ScannedProduct(new ScannedProduct(125, "Hiper-Mor", DateTime.Now.AddDays(2),25, 3));
            rep.add_ScannedProduct(new ScannedProduct(125, "Hiper-Mor", DateTime.Now.AddDays(40), 25, 3));



        }



        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {    
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
            ButtonOpenMenu.Foreground = Brushes.White;
            ButtonCloseMenu.Foreground = Brushes.White;
            closeMenuStoryBoard();
        }

        internal void LoadLastProductsView()
        {
            LastProductsUserControlV LP = new LastProductsUserControlV();
            CurrnetUserControl = LP;
            ButtonOpenMenu.Foreground = Brushes.Black;
            ButtonCloseMenu.Foreground = Brushes.Black;
            closeMenuStoryBoard();
        }
        
        internal void LoadPurchaseHistoryView()
        {
            PurchaseHistoryV PH = new PurchaseHistoryV();
            CurrnetUserControl = PH;
            ButtonOpenMenu.Foreground = Brushes.Black;
            ButtonCloseMenu.Foreground = Brushes.Black;
            closeMenuStoryBoard();
        }


        internal void LoadPurchaseTogetherView()
        {
            PurchasedTogetherUserControlV PT = new PurchasedTogetherUserControlV();
            CurrnetUserControl = PT;
            ButtonOpenMenu.Foreground = Brushes.Black;
            ButtonCloseMenu.Foreground = Brushes.Black;
            closeMenuStoryBoard();
        }
        internal void LoadStatisticsView()
        {
            StatisticsUserControlV S = new StatisticsUserControlV();
            CurrnetUserControl = S;
            ButtonOpenMenu.Foreground = Brushes.Black;
            ButtonCloseMenu.Foreground = Brushes.Black;
            closeMenuStoryBoard();
        }


        internal void LoadRecommendedShoppingView()
        {
            RecommendedShoppingUserControlV RS = new RecommendedShoppingUserControlV();
            CurrnetUserControl = RS;
            ButtonOpenMenu.Foreground = Brushes.Black;
            ButtonCloseMenu.Foreground = Brushes.Black;
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
    