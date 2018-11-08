using System.Net;
using Acr.UserDialogs;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism.Unity;
using Prism.Ioc;
using Randevy.Infrastructure.Constants;
using Randevy.Infrastructure.Extensions;
using Randevy.Infrastructure.Interfaces;
using Randevy.Infrastructure.Interfaces.App;
using Randevy.Services;
using Randevy.Services.App;
using Randevy.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Randevy
{
    public partial class App : PrismApplication
    {
        private ILocalStorageService _localStorageService;
        public static T Resolve<T>()
        {
            return (Current as App).Container.Resolve<T>();
        }


        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #region Prism

        /// <summary>
        /// Register DI
        /// </summary>
        /// <param name="container"></param>
        protected override void RegisterTypes(IContainerRegistry container)
        {
            //Navigation
            container.RegisterForNavigation<NavigationPage>();
            container.RegisterForNavigation<MainView>(nameof(MainView));
            container.RegisterForNavigation<UsersView>(nameof(UsersView));
            container.RegisterForNavigation<UserView>(nameof(UserView));
            container.RegisterForNavigation<LoginView>(nameof(LoginView));


            //Services
            container.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
            container.RegisterInstance<ISettings>(CrossSettings.Current);

            container.Register<IJsonService, JsonService>();
            container.Register<IHttpService, HttpService>();
            container.Register<IHttpService, HttpService>();
            container.Register<ILocalStorageService, LocalStorageService>();


            container.Register<IUsersService, UsersService>();
        }


        protected override async void OnInitialized()
        {
            _localStorageService = Resolve<ILocalStorageService>();

            var token = _localStorageService.Load<string>(Constants.StorageKeys.Token);
            if (!token.IsNotNullOrEmpty())
                await NavigationService.NavigateAsync("/" + string.Join("/", nameof(NavigationPage), nameof(LoginView)));
            else
            {
                await NavigationService.NavigateAsync("/" + string.Join("/", nameof(NavigationPage), nameof(MainView)));
            }
        }


        #endregion

    }
}
