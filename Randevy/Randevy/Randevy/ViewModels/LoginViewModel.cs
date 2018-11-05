using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Newtonsoft.Json;
using Prism.Navigation;
using Randevy.Infrastructure.Constants;
using Randevy.Infrastructure.Interfaces;
using Randevy.Infrastructure.Interfaces.App;
using Randevy.Models;
using Randevy.Models.Auth;
using Randevy.Views;
using Xamarin.Forms;

namespace Randevy.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorageService;

        public LoginViewModel(
            IHttpService httpService,
            INavigationService navigationService,
            ILocalStorageService localStorageService) : base(navigationService)
        {
            _httpService = httpService;
            _localStorageService = localStorageService;
            _login = new LoginModel();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private LoginModel _login;
        public LoginModel Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        
        private ICommand _loginCommand;

        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(LoginHandler));

        private async void LoginHandler(object obj)
        {
            var res = await _httpService.PostAsync<TokenModel>("api/account/login", this._login, false);
            _localStorageService.Save(Constants.StorageKeys.Token, res.Data.Token);
            await NavigationService.NavigateAsync(nameof(MainView));
        }
    }
}
