using ShopApp.Views;

namespace ShopApp
{
    public partial class App : Application
    {
        private readonly LoginPage _loginPage;
        private readonly AppShell _appShell;

        public App(LoginPage loginPage, AppShell appShell)
        {
            InitializeComponent();
            _loginPage = loginPage;
            _appShell = appShell;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var accessToken = Preferences.Get("accesstoken", string.Empty);

            if (string.IsNullOrEmpty(accessToken))
            {
                // Si no hay token, muestra la LoginPage.
                return new Window(_loginPage);
            }
            else
            {
                // Si hay token, muestra el contenedor principal (AppShell).
                return new Window(_appShell);
            }
        }
    }
}