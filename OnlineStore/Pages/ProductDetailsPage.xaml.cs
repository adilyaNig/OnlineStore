using OnlineStore.DataBase;
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

namespace OnlineStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductDetailsPage.xaml
    /// </summary>
    public partial class ProductDetailsPage : Page
    {
        public User CurrentUser { get; set; }
        public Product CurrentProduct { get; set; }
        public string CategoryName { get; set; }

        public ProductDetailsPage(User user, Product product)
        {
            InitializeComponent();
            CurrentUser = user;
            CurrentProduct = product;
            CategoryName = DBConn.connection.Category
                .FirstOrDefault(c => c.CategoryID == product.CategoryID)?.Name;
            DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(CurrentUser));
        }

        

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            // Реализация добавления в корзину
            NavigationService.Navigate(new AddToCartPage(CurrentUser, CurrentProduct));
        }

        private void btnViewReviews_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReviewsPage(CurrentUser, CurrentProduct));
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddReviewPage(CurrentUser, CurrentProduct));
        }
    }
}
