using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShopApp.ViewModels;
public class HelpSupportViewModel : BindingUtilObject
{
    private readonly INavegacionService _navegacionService;
    public HelpSupportViewModel(INavegacionService navegacionService)
    {
        var dataBase = new ShopDbContext();
        Clients = new ObservableCollection<Client>([.. dataBase.Clients]);
        PropertyChanged += HelpSupportData_PropertyChanged;
        _navegacionService = navegacionService;
    }

    private async void HelpSupportData_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ClienteSeleccionado))
        {
            var uri = $"{nameof(HelpSupportDetailPage)}?id={ClienteSeleccionado.Id}";
            await _navegacionService.GoToAsync(uri);
        }
    }

    public int _visitasPendientes;
    public int VisitasPendientes
    {
        get
        {
            return _visitasPendientes;
        }
        set
        {
            if (_visitasPendientes != value)
            {
                _visitasPendientes = value;
                RaisePropertyChanged();
            }
        }
    }

    private ObservableCollection<Client> _clients;

    public ObservableCollection<Client> Clients
    {
        get
        {
            return _clients;
        }
        set
        {
            if (_clients != value)
            {
                _clients = value;
                RaisePropertyChanged();
            }
        }
    }

    private Client _clienteSeleccionado;

    public Client ClienteSeleccionado
    {
        get
        {
            return _clienteSeleccionado;
        }
        set
        {
            if (_clienteSeleccionado != value)
            {
                _clienteSeleccionado = value;
                RaisePropertyChanged();
            }
        }
    }
}

