using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using FoodOrderApp.Models;
namespace FoodOrderApp
{
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<CartItem> Cart { get; set; }

        public Command<CartItem> RemoveFromCartCommand { get; }
        public Command PlaceOrderCommand { get; }

        public CartPage(ObservableCollection<CartItem> cart)
        {
            InitializeComponent();
            Cart = cart;
            RemoveFromCartCommand = new Command<CartItem>((item) => RemoveFromCart(item));
            PlaceOrderCommand = new Command(() => PlaceOrder());
            BindingContext = this;
        }

        private void RemoveFromCart(CartItem cartItem)
        {
            Cart.Remove(cartItem);
        }

        private async void PlaceOrder()
        {
            await Navigation.PushAsync(new PaymentPage(Cart));
        }
    }
}
