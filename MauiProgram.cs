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
                .AddSingleton<HttpClient>();

#if ANDROID
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.Android.DatabaseRutaService>();
#elif IOS
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.iOS.DatabaseRutaService>();
#elif WINDOWS
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.Windows.DatabaseRutaService>();
#endif

            builder.Services
                .AddDbContext<ShopOutDbContext>()
                .AddDbContext<ShopDbContext>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var shopOutContext = serviceProvider.GetRequiredService<ShopOutDbContext>();
                shopOutContext.Database.EnsureCreated();

                var shopContext = serviceProvider.GetRequiredService<ShopDbContext>();
                shopContext.Database.EnsureCreated();
            }

            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
            Routing.RegisterRoute(nameof(HelpSupportDetailPage), typeof(HelpSupportDetailPage));

            return app;
        }
    }
}
