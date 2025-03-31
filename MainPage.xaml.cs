using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using FoodOrderApp.Models;
namespace FoodOrderApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<CartItem> Cart { get; set; }

        public Command<Product> AddToCartCommand { get; }
        public Command NavigateToCartCommand { get; }

        public MainPage()
        {
            InitializeComponent();

            Products = new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Пицца", Price = 350 },
                new Product { Id = 2, Name = "Бургер", Price = 150 },
                new Product { Id = 3, Name = "Салат", Price = 120 },
                new Product { Id = 4, Name = "Суп", Price = 100 }
            };

            Cart = new ObservableCollection<CartItem>();

            AddToCartCommand = new Command<Product>((product) => AddToCart(product));
            NavigateToCartCommand = new Command(() => NavigateToCart());

            BindingContext = this;
        }

        private void AddToCart(Product product)
        {
            var cartItem = Cart.FirstOrDefault(c => c.Product.Id == product.Id);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                Cart.Add(new CartItem { Product = product, Quantity = 1 });
            }
        }

        private async void NavigateToCart()
        {
            await Navigation.PushAsync(new CartPage(Cart));
        }
    }
}
