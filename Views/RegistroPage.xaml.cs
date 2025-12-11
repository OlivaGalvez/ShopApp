using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class RegistroPage : ContentPage
{
	public RegistroPage(RegistroViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}