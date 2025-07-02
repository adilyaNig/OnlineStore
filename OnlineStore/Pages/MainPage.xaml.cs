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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public User CurrentUser { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        public MainPage(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            LoadData();
            DataContext = this;
        }

        private void LoadData()
        {
            Products = DBConn.connection.Product.ToList();
            Categories = DBConn.connection.Category.ToList();

            lvProducts.ItemsSource = Products;
            cbCategories.ItemsSource = Categories;
            cbCategories.SelectedIndex = -1;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterProducts();
        }

        private void cbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterProducts();
        }

        private void FilterProducts()
        {
            var filtered = DBConn.connection.Product.AsQueryable();

            // Filter by search text
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                filtered = filtered.Where(p => p.Name.Contains(tbSearch.Text) ||
                                             p.Description.Contains(tbSearch.Text));
            }

            // Filter by category
            if (cbCategories.SelectedItem is Category selectedCategory)
            {
                filtered = filtered.Where(p => p.CategoryID == selectedCategory.CategoryID);
            }

            lvProducts.ItemsSource = filtered.ToList();
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProducts.SelectedItem is Product selectedProduct)
            {
                if (CurrentUser.Role == "Admin" || CurrentUser.Role == "Manager")
                {
                    NavigationService.Navigate(new EditProductPage(CurrentUser, selectedProduct));
                }
                else
                {
                    NavigationService.Navigate(new ProductDetailsPage(CurrentUser, selectedProduct));
                }
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.Role == "Admin" || CurrentUser.Role == "Manager")
            {
                NavigationService.Navigate(new AddProductPage(CurrentUser));
            }
            else
            {
                MessageBox.Show("У Вас нет разрешения на добавление товаров");
            }
        }

        private void btnViewOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage(CurrentUser));
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }
    }
}
