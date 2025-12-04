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

    public HelpSupportDetailsViewModel(IConnectivity connectivity, CompraService compraService)
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
    }

    [RelayCommand(CanExecute = nameof(StatusConnection))]
    private async Task EnviarCompra()
    {
        var resultado = await _compraService.EnviarData(Compras);
        if (resultado)
        {
            await Shell.Current.DisplayAlert("Mensaje", "Se enviaron las compras al servidor backend", "OK");
        }
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
