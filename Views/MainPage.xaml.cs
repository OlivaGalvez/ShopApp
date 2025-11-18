using ShopApp.DataAccess;

namespace ShopApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var dbContext = new ShopDbContext();
            category.Text = dbContext.Categories.Count().ToString();
            product.Text = dbContext.Products.Count().ToString();
            client.Text = dbContext.Clients.Count().ToString();
        }

        //private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        //{
        //    //await DisplayAlert("Mensaje de Vaxi", "Hola VaxiDrez", "OK", "Cancelar");
        //    (sender as Rectangle)?.ScaleTo(2);
        //    (sender as Rectangle)?.TranslateTo(200,200);
        //}
    }
}
