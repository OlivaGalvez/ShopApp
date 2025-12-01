using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAccess;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopApp.ViewModels;
public partial class HelpSupportDetailsViewModel : ViewModelGlobal
{
    [ObservableProperty]
    private ObservableCollection<Compra> compras = [];

    [ObservableProperty]
    private int clientId;

    [ObservableProperty]
    private ObservableCollection<Product> products;

    [ObservableProperty]
    private Product productoSeleccionado;

    [ObservableProperty]
    private int cantidad;

    public HelpSupportDetailsViewModel()
    {
        var dataBase = new ShopDbContext();
        Products = new ObservableCollection<Product>(dataBase.Products);
        AddCommand = new Command(() =>
        {
            var compra = new Compra(ClientId, ProductoSeleccionado.Id, Cantidad);
            Compras.Add(compra);
        },
        () => true
        );
    }

    public ICommand AddCommand { get; set; }
}
