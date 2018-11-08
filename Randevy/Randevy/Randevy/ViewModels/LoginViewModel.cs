using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
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
using Xamarin.Forms.Xaml;

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

        private bool _load;
        public bool Load
        {
            get => _load;
            set => SetProperty(ref _load, value);
        }

        private bool _valid;
        public bool Valid
        {
            get => _valid;
            set => SetProperty(ref _valid, value);
        }

        private string _err;
        public string ErrMessage
        {
            get => _err;
            set => SetProperty(ref _err, value);
        }

        private ICommand _loginCommand;

        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(LoginHandler));

        private async void LoginHandler(object obj)
        {
            Load = true;
            Valid = false;

            var res = await _httpService.PostAsync<TokenModel>("api/account/login", this._login, false);

            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                _localStorageService.Save(Constants.StorageKeys.Token, res.Data.Token);
                await NavigationService.NavigateAsync(nameof(MainView));
            }

            if (res.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                Valid = true;
                ErrMessage = "Smth went wrong";
            }

            Load = false;
        }
    }
}
