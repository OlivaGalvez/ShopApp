using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Models.Backend.Login;
using ShopApp.Services;
using System.Text.RegularExpressions;

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

    private readonly SecurityService _securityService;

    public RegistroViewModel(SecurityService securityService)
    {
        _securityService = securityService;
    }


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

        if (!ValidarEmail(Email))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Campo email incorrecto.", "Aceptar");
            return;
        }

        if (!ValidarContrasenia(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "La contraseña debe tener al menos 12 caracteres, una mayúscula, una minúscula, un número y un carácter especial.", "Aceptar");
            return;
        }

        RegistroRequest registroRequest = new ()
        {
            Nombre = Nombre,
            Apellido = Apellido,
            Email = Email,
            UserName = Username,
            Telefono = Telefono,
            Password = Password
        };

        var resultado = await _securityService.Registro(registroRequest);

        if (resultado)
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await Shell.Current.DisplayAlert("Mensaje", "Error al realizar el registro", "Aceptar");
        }
    }

    private bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    private bool ValidarContrasenia(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (password.Length < 12)
            return false;

        if (!Regex.IsMatch(password, @"[A-Z]"))
            return false;

        if (!Regex.IsMatch(password, @"[a-z]"))
            return false;

        if (!Regex.IsMatch(password, @"\d"))
            return false;

        if (!Regex.IsMatch(password, @"[\W_]")) 
            return false;

        return true;
    }

    [RelayCommand]
    private async Task NavegarLogin()
    {
        await Shell.Current.GoToAsync("LoginPage");
    }
}
