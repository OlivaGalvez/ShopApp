using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.Models.Backend.Inmueble;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ShopApp.ViewModels;

public partial class InmuebleListViewModel : ViewModelGlobal, IQueryAttributable
{
    [ObservableProperty]
    InmuebleResponse inmuebleSeleccionado;

    [ObservableProperty]
    ObservableCollection<InmuebleResponse> inmuebles;

    private readonly INavegacionService _navegacionService;

    private readonly InmuebleService _inmuebleService;

    public InmuebleListViewModel(INavegacionService navegacionService, InmuebleService inmuebleService)
    {
        _navegacionService = navegacionService;
        _inmuebleService = inmuebleService;
        PropertyChanged += OnPropertyChanged;
    }

    private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InmuebleSeleccionado))
        {
            var uri = $"{nameof(InmuebleDetailPage)}?id={InmuebleSeleccionado.Id}";
            await _navegacionService.GoToAsync(uri);
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        await LoadDataAsync(id);
    }

    public async Task LoadDataAsync(int category)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var listInmuebles = await _inmuebleService.GetInmuebleByCategory(category);
            if (listInmuebles != null)
            {
                Inmuebles = new ObservableCollection<InmuebleResponse>(listInmuebles);
            }
        }
        catch(Exception e)
        {
            await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }

    }
}
