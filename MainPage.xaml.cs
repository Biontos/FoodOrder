using System.Collections.ObjectModel;
using System.Linq;
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
                new Product { Id = 1, Name = "Яса Сиса", Price = 1899.0, Quantity = 1, ImageUrl = "https://977dba4f-c570-41cd-b9a9-7e6dd1fedbf6.selcdn.net/images/gB9_rMdtPTpGy5X5W9LMbZT6jzwoxf78pMZp1y7t4wY/pr:medium/M2luenA0M3Z1MGVy/cmVhenYzdTZlOGxi/eHR3ag"},
                new Product { Id = 2, Name = "Суки Ё", Price = 1799.0, Quantity = 1, ImageUrl = "https://977dba4f-c570-41cd-b9a9-7e6dd1fedbf6.selcdn.net/images/74Pm9C_yJIK0d6RDUocbWa_aRXVc_y3-HayX0UeHR08/pr:medium/bHZqYng5eGY2b3N3/MjduNnhhNm55aDgw/a2c3Mw"},
                new Product { Id = 3, Name = "Тити Оки", Price = 2599.0, Quantity = 1, ImageUrl = "https://977dba4f-c570-41cd-b9a9-7e6dd1fedbf6.selcdn.net/images/4cB3MMUBbuihRw5ckvz-0oBzLbnwfjT7qvdwpzroTkQ/pr:medium/NWxhbzFwYzNuc2xw/cnhoamhvcG9xNm92/dnJqeQ" },
                new Product { Id = 4, Name = "Куни Ли", Price = 2269.0, Quantity = 1, ImageUrl = "https://977dba4f-c570-41cd-b9a9-7e6dd1fedbf6.selcdn.net/images/SreRIjozhhfnQ1_Ol-ewbels42iuqWPTDYOXk7b44pg/pr:medium/NmVrcjF1Z2dpbGNl/a3o4MTV4MzFiOWJz/OW8xYQ" },
                new Product { Id = 5, Name = "БиЧ", Price = 1349.0, Quantity = 1, ImageUrl = "https://977dba4f-c570-41cd-b9a9-7e6dd1fedbf6.selcdn.net/images/st0ASChLffUEM2xy2u_xNzL2nipXNjkBTR2MCbyVWoc/pr:medium/eTQwcnBla3RpbHlw/dnNnbmV2Njg0aDIz/Z3kxcQ"},
                new Product { Id = 6, Name = "Чпо Ки", Price = 2099.0, Quantity = 1,  ImageUrl = "https://977dba4f-c570-41cd-b9a9-7e6dd1fedbf6.selcdn.net/images/ut6gQr-Sj78PiM8P8ErWLgfR_ULsmDzpaKDxfwYzzZg/pr:medium/aDZydjA3Mnkzazhk/OGRkdTJtZmx5Y3Ni/d3NvNg" },
                new Product { Id = 7, Name = "Ваху Яки", Price = 1469.0, Quantity = 1, ImageUrl = "https://977dba4f-c570-41cd-b9a9-7e6dd1fedbf6.selcdn.net/images/-uM1h_WuUz5SfzDf6aTyTkhsAvWi1UEJDJPjC_oDgWE/pr:medium/YWM4Y2VqYmFwMjVj/bXZ4dGhwYmxyd3lo/djZoOA" },
                new Product { Id = 8, Name = "Си Сяке", Price = 2269.0, Quantity = 1, ImageUrl = "https://977dba4f-c570-41cd-b9a9-7e6dd1fedbf6.selcdn.net/images/jfyBngSbqqd48OYeZnIfPbExoXhvTP0G-R9n7UM-5GA/pr:medium/bGhoaDFhd2kyY2V1/dm9nbmYza3cyb25s/ZzFzYg" },
        };

            Cart = new ObservableCollection<CartItem>();

            AddToCartCommand = new Command<Product>((product) => AddToCart(product));
            NavigateToCartCommand = new Command(() => NavigateToCart());

            BindingContext = this;
        }

        private async void AddToCart(Product product)
        {
            int quantity = product.Quantity;

            if (quantity > 0)
            {
                var cartItem = Cart.FirstOrDefault(c => c.Product.Id == product.Id);

                if (cartItem != null)
                {
                    cartItem.Quantity += quantity; 
                }
                else
                {
                    Cart.Add(new CartItem { Product = product, Quantity = quantity }); 
                }

                UpdateTotalPrice();

                await DisplayAlert("Уведомление", $"{product.Name} добавлен в корзину в количестве {quantity}.", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите корректное количество.", "OK");
            }
        }

        private void UpdateTotalPrice()
        {
            decimal totalPrice = Cart.Sum(cartItem => (decimal)cartItem.Product.Price * cartItem.Quantity);
            TotalPriceLabel.Text = $"Итоговая цена: {totalPrice:C}";
        }

        private async void NavigateToCart()
        {
            await Navigation.PushAsync(new CartPage(Cart));
        }
    }
}
