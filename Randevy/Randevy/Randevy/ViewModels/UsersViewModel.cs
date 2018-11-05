using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using Randevy.Infrastructure.Interfaces;
using Randevy.Models;
using Randevy.Models.App;
using Randevy.Views;
using Xamarin.Forms;

namespace Randevy.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly IUsersService _usersService;
        private readonly IUserDialogs _dialogs;

        public UsersViewModel(
            IUsersService usersService,
            INavigationService navigationService,
            IUserDialogs dialogs)
            : base(navigationService)
        {
            _usersService = usersService;
            _dialogs = dialogs;
        }


        private ObservableCollection<UserModel> _users = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        private string _search;
        public string Search
        {
            get => _search;
            set => SetProperty(ref _search, value);
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            var res = await _usersService.GetUsers();
            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                Users = new ObservableCollection<UserModel>(res.Data.Data);
            }

            if (res.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                await NavigationService.NavigateAsync(nameof(LoginView));
            }

            base.OnNavigatingTo(parameters);
        }

        private ICommand _goToMainCommand;
        public ICommand GoToMainCommand => 
            _goToMainCommand ?? (_goToMainCommand = new Command(GoToMainCommandHandler));

        private async void GoToMainCommandHandler(object obj)
        {
            await NavigationService.NavigateAsync(nameof(MainView));
        }

        private ICommand _searchCommand;
        public ICommand Searching =>
            _searchCommand ?? (_searchCommand = new Command(SearchHandler));

        private async void SearchHandler(object obj)
        {
            var res = await _usersService.GetUsers(new FilterBase { Search = _search });
            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                Users = new ObservableCollection<UserModel>(res.Data.Data);
            }
        }
       
    }
}