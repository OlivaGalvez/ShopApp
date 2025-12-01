using ShopApp.ViewModels;
namespace ShopApp.Views;

public partial class HelpSupportDetailPage : ContentPage
{
	public HelpSupportDetailPage(HelpSupportDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}