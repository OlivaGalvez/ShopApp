using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAccess;
using System.Collections.ObjectModel;
namespace ShopApp.ViewModels;

public partial class ClientsViewModel : ViewModelGlobal
{
    [ObservableProperty]
    ObservableCollection<Client> clients;

    [ObservableProperty]
    Client clientSeleccionado;

    public ClientsViewModel()
    {
        var dataBase = new ShopDbContext();
        Clients = new ObservableCollection<Client>(dataBase.Clients);
    }
}
