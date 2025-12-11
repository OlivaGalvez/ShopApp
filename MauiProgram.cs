using Microsoft.Extensions.Configuration;
using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.ViewModels;
using ShopApp.Views;
using System.Reflection;

namespace ShopApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var assemblyInstance = Assembly.GetExecutingAssembly();
            using var stream = assemblyInstance.GetManifestResourceStream("ShopApp.appsettings.json");
            
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Configuration.AddConfiguration(config);

            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<ShopOutDbContext>();
            builder.Services.AddTransient<ShopDbContext>();

            builder.Services.AddSingleton<INavegacionService, NavegacionService>();

            // ViewModels
            builder.Services
                .AddTransient<HelpSupportViewModel>()
                .AddTransient<HelpSupportDetailsViewModel>()
                .AddTransient<ClientsViewModel>()
                .AddTransient<ProductDetailsViewModel>()
                .AddTransient<ProductsViewModel>()
                .AddTransient<ResumenViewModel>()
                .AddTransient<LoginViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<BookmarkViewModel>()
                .AddTransient<SettingsViewModel>()
                .AddTransient<InmuebleListViewModel>();

            // Pages 
            builder.Services
                .AddTransient<HelpSupportPage>()
                .AddTransient<HelpSupportDetailPage>()
                .AddTransient<ClientsPage>()
                .AddTransient<ProductDetailPage>()
                .AddTransient<ProductsPage>()
                .AddTransient<ResumenPage>()
                .AddTransient<LoginPage>()
                .AddTransient<HomePage>()
                .AddTransient<BookmarkPage>()
                .AddTransient<SettingsPage>()
                .AddTransient<InmuebleListPage>(); 

            // Services y otras dependencias (Ciclo de vida mantenido)
            builder.Services
                .AddSingleton(Connectivity.Current)
                .AddSingleton<CompraService>()
                .AddSingleton<InmuebleService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<SecurityService>();


#if ANDROID
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.Android.DatabaseRutaService>();
#elif IOS
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.iOS.DatabaseRutaService>();
#elif WINDOWS
            builder.Services.AddSingleton<IDatabaseRutaService, Platforms.Windows.DatabaseRutaService>();
#endif

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
            Routing.RegisterRoute(nameof(InmuebleListPage), typeof(InmuebleListPage));

            return app;
        }
    }
}
