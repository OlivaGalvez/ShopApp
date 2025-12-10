using Microsoft.Extensions.Logging;
using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.ViewModels;
using ShopApp.Views;

namespace ShopApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<INavegacionService, NavegacionService>()
                .AddTransient<HelpSupportViewModel>()
                .AddTransient<HelpSupportDetailsViewModel>()
                .AddTransient<ClientsViewModel>()
                .AddTransient<ProductDetailsViewModel>()
                .AddTransient<ProductsViewModel>()
                .AddTransient<ResumenViewModel>()
                .AddTransient<HelpSupportPage>()
                .AddTransient<HelpSupportDetailPage>()
                .AddTransient<ClientsPage>()
                .AddTransient<ProductDetailPage>()
                .AddTransient<ProductsPage>()
                .AddTransient<ResumenPage>()
                .AddSingleton(Connectivity.Current)
                .AddSingleton<CompraService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<ShopOutDbContext>();

#if ANDROID
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.Android.DatabaseRutaService>();
#elif IOS
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.iOS.DatabaseRutaService>();
#elif WINDOWS
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.Windows.DatabaseRutaService>();
#endif

            var dbContext = new ShopDbContext();
            //Crear bbdd en memoria
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
            Routing.RegisterRoute(nameof(HelpSupportDetailPage), typeof(HelpSupportDetailPage));


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
