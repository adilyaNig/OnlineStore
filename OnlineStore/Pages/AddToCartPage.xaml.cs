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
    /// Логика взаимодействия для AddToCartPage.xaml
    /// </summary>
    public partial class AddToCartPage : Page
    {
        public User CurrentUser { get; set; }
        public Product Product { get; set; }

        public AddToCartPage(User user, Product product)
        {
            InitializeComponent();
            CurrentUser = user;
            Product = product;
            DataContext = this;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbQuantity.Text, out int quantity) && quantity > 0)
            {
                // Здесь должна быть логика добавления в корзину
                MessageBox.Show("Товар добавлен в корзину!");
                NavigationService.Navigate(new MainPage(CurrentUser));
            }
            else
            {
                MessageBox.Show("Введите валидное значение");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductDetailsPage(CurrentUser, Product));
        }
    }
}
