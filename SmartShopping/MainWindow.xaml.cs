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

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowVM(this);
            LoadHomeView();
            DAL.Repository rep= new DAL.Repository();
            rep.add_Product(new Product("kuku1", @"url1", Category.drinks));
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


        public void closeMenuStoryBoard()
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
            sb.Begin();
        }

    }
}
