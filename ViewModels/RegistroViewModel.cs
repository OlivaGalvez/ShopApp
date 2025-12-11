using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopApp.ViewModels;

public partial class RegistroViewModel : ViewModelGlobal
{
    [ObservableProperty]
    private string nombre;

    [ObservableProperty]
    private string apellido;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string telefono;

    [ObservableProperty]
    private string password;

    [RelayCommand]
    private async Task RegistrarUsuario()
    {
        if (string.IsNullOrWhiteSpace(Nombre) ||
            string.IsNullOrWhiteSpace(Apellido) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Username) ||
            string.IsNullOrWhiteSpace(Telefono) ||
            string.IsNullOrWhiteSpace(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "Aceptar");
            return;
        }

        // Lógica para registrar al usuario...

        await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario registrado correctamente.", "Aceptar");
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task NavegarLogin()
    {
        await Shell.Current.GoToAsync("LoginPage");
    }
}
