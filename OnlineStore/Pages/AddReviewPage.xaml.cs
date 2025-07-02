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
    /// Логика взаимодействия для AddReviewPage.xaml
    /// </summary>
    public partial class AddReviewPage : Page
    {
        public User CurrentUser { get; set; }
        public Product Product { get; set; }

        public AddReviewPage(User user, Product product)
        {
            InitializeComponent();
            CurrentUser = user;
            Product = product;
            DataContext = this;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbReview.Text))
            {
                MessageBox.Show("Enter your review");
                return;
            }

            var rating = int.Parse((cbRating.SelectedItem as ComboBoxItem).Content.ToString());

            var review = new Review
            {
                UserID = CurrentUser.UserID,
                ProductID = Product.ProductID,
                Rating = rating,
                ReviewText = tbReview.Text,
                ReviewDate = DateTime.Now
            };

            DBConn.connection.Review.Add(review);
            DBConn.connection.SaveChanges();

            MessageBox.Show("Review added!");
            NavigationService.Navigate(new ReviewsPage(CurrentUser, Product));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReviewsPage(CurrentUser, Product));
        }
    }
}
