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
    /// Логика взаимодействия для ReviewsPage.xaml
    /// </summary>
    public partial class ReviewsPage : Page
    {
        public User CurrentUser { get; set; }
        public Product Product { get; set; }

        public ReviewsPage(User user, Product product)
        {
            InitializeComponent();
            CurrentUser = user;
            Product = product;
            DataContext = this;
            LoadReviews();
        }

        private void LoadReviews()
        {
            lvReviews.ItemsSource = DBConn.connection.Review
                .Where(r => r.ProductID == Product.ProductID)
                .ToList();
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddReviewPage(CurrentUser, Product));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductDetailsPage(CurrentUser, Product));
        }
    }
}
