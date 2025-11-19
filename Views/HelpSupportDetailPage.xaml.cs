
using ShopApp.DataAccess;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopApp.Views;

public partial class HelpSupportDetailPage : ContentPage, IQueryAttributable
{
	public HelpSupportDetailPage()
	{
		InitializeComponent();
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Title = $"Cliente: {query["id"]}";

        var clientId = int.Parse(query["id"].ToString());
        (BindingContext as HelpSupportDetailsData).ClientId = clientId;
    }
}

public class HelpSupportDetailsData : BindingUtilObject
{
    public HelpSupportDetailsData()
    {
        var dataBase = new ShopDbContext();
        Products = new ObservableCollection<Product>(dataBase.Products);
        AddCommand = new MiComando(() =>
        { 
            var compra = new Compra(ClientId, ProductoSeleccionado.Id, Cantidad);
            Compras.Add(compra);
        },
        () => true
        );
    }

    public ICommand AddCommand { get; set; }

    private ObservableCollection<Compra> _compras = [];

    public ObservableCollection<Compra> Compras
    {
        get => _compras;
        set
        {
            if (_compras != value)
            {
                _compras = value;
                RaisePropertyChanged();
            }
        }
    }

    private int _clientId { get; set; }

    public int ClientId
    {
        get => _clientId;
        set
        {
            if (_clientId != value)
            {
                _clientId = value;
                RaisePropertyChanged();
            }
        }
    }


    private ObservableCollection<Product> _products;

    public ObservableCollection<Product> Products
    {
        get => _products;
        set 
        { 
            if (_products != value)
            {
                _products = value;
                RaisePropertyChanged();
            }
        }
    }

    private Product _productoSeleccionado;

    public Product ProductoSeleccionado
    {
        get => _productoSeleccionado;
        set
        {
            if (_productoSeleccionado != value)
            {
                _productoSeleccionado = value;
                RaisePropertyChanged();
            }
        }
    }

    private int _cantidad;
    public int Cantidad
    {
        get => _cantidad;
        set
        {
            if (_cantidad != value)
            {
                _cantidad = value;
                RaisePropertyChanged();
            }
        }
    }
}