using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShopApp.ViewModels;

public partial class ProductsViewModel : ViewModelGlobal
{
    private readonly INavegacionService navigationService;

    [ObservableProperty]
    ObservableCollection<Product> products;

    [ObservableProperty]
    Product productoSeleccionado;

    public ProductsViewModel(INavegacionService navigationService)
    {
        this.navigationService = navigationService;
        CargarListaProductos();
        PropertyChanged += ProductsViewModel_PropertyChanged;
    }

    private async void ProductsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(ProductoSeleccionado))
        {
            var uri = $"{nameof(ProductDetailPage)}?id={ProductoSeleccionado.Id}";
            await navigationService.GoToAsync(uri);
        }
    }

    private void CargarListaProductos()
    {
        var dataBase = new ShopDbContext();
        Products = new ObservableCollection<Product>(dataBase.Products);
        dataBase.Dispose();
    }
}
