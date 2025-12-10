using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.DataAccess;
using ShopApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopApp.ViewModels;
public partial class HelpSupportDetailsViewModel : ViewModelGlobal, IQueryAttributable
{
    private readonly IConnectivity _connectivity;
    private readonly CompraService _compraService;
    private readonly ShopOutDbContext _outDbContext;

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

    public HelpSupportDetailsViewModel(
        IConnectivity connectivity, 
        CompraService compraService,
        ShopOutDbContext outDbContext)
    {
        var dataBase = new ShopDbContext();
        Products = new ObservableCollection<Product>(dataBase.Products);
        AddCommand = new Command(() =>
        {
            var compra = new Compra(
                ClientId, 
                ProductoSeleccionado.Id, 
                Cantidad,
                ProductoSeleccionado.Nombre,
                ProductoSeleccionado.Precio,
                ProductoSeleccionado.Precio * Cantidad
                );
            Compras.Add(compra);
        },
        () => true
        );
        _connectivity = connectivity;
        _connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        _compraService = compraService;
        _outDbContext = outDbContext;
    }

    [RelayCommand(CanExecute = nameof(StatusConnection))]
    private async Task EnviarCompra()
    {
        _outDbContext.Database.EnsureCreated();

        foreach (var compra in Compras)
        {
            _outDbContext.Compras.Add(new CompraItem(
                compra.ClientId,
                compra.ProductId,
                compra.Cantidad,
                compra.ProductoPrecio
                ));
        }

        await _outDbContext.SaveChangesAsync();
        await Shell.Current.DisplayAlert("Éxito", "Las compras se han guardado localmente.", "OK");
    }

    private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        EnviarCompraCommand.NotifyCanExecuteChanged();
    }

    private bool StatusConnection()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet;
    }

    public ICommand AddCommand { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var clientId = int.Parse(query["id"].ToString());
        ClientId = clientId;
    }

    [RelayCommand]
    private void EliminarCompra(Compra compra)
    {
        Compras.Remove(compra);
    }
}
