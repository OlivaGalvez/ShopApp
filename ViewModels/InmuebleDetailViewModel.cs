using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.Models.Backend.Inmueble;
using ShopApp.Services;

namespace ShopApp.ViewModels;

public partial class InmuebleDetailViewModel : ViewModelGlobal, IQueryAttributable
{
    [ObservableProperty]
    private InmuebleResponse inmueble;

    private InmuebleService _inmuebleService;

    private readonly INavegacionService _navegacionService;

    public InmuebleDetailViewModel(InmuebleService inmuebleService, INavegacionService navegacionService)
    {
        _inmuebleService = inmuebleService;
        _navegacionService = navegacionService;
    }

    public async Task LoadDataAsync(int inmuebleId)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Inmueble = await _inmuebleService.GetInmuebleById(inmuebleId);
        }
        catch (Exception e)
        {
            await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        await LoadDataAsync(id);
    }
}
