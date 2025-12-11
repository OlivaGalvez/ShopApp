using CommunityToolkit.Mvvm.Input;

namespace ShopApp.ViewModels;

public partial class RegistroViewModel : ViewModelGlobal
{
    [RelayCommand]
    private async Task NavegarLogin()
    {
        await Shell.Current.GoToAsync("LoginPage");
    }
}
