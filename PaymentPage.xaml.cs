using System.Collections.ObjectModel;
using FoodOrderApp.Models;
namespace FoodOrderApp
{
    public partial class PaymentPage : ContentPage
    {
        public ObservableCollection<CartItem> Cart { get; set; }
        public List<string> PaymentMethods { get; set; }  
        public string SelectedPaymentMethod { get; set; }  

        public Command ConfirmOrderCommand { get; }

        public PaymentPage(ObservableCollection<CartItem> cart)
        {
            InitializeComponent();

            Cart = cart;

            PaymentMethods = new List<string>
            {
                "���������� ������",
                "������ ������",
            };

            BindingContext = this;

            ConfirmOrderCommand = new Command(ConfirmOrder);
        }

        private async void ConfirmOrder()
        {
            if (string.IsNullOrEmpty(SelectedPaymentMethod))
            {
                await DisplayAlert("������", "�������� ������ ������", "OK");
                return;
            }

            await DisplayAlert("����� ��������",
                $"��� ����� ��������. ������ ������: {SelectedPaymentMethod}.", "OK");

            await Navigation.PopToRootAsync(); 
        }
    }
}
