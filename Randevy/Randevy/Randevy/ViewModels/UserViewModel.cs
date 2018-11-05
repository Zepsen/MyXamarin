using System.Collections.Generic;
using System.Net;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using Randevy.Infrastructure.Interfaces;
using Randevy.Models;
using Randevy.Views;
using Xamarin.Forms;

namespace Randevy.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IUsersService _usersService;
        private readonly IUserDialogs _dialogs;

        public UserViewModel(
            IUsersService usersService,
            INavigationService navigationService,
            IUserDialogs dialogs)
            : base(navigationService)
        {
            _usersService = usersService;
            _dialogs = dialogs;
        }


        private UserModel _user = new UserModel();
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        

        private ICommand _getCommand;
        public ICommand Get =>
            _getCommand ?? (_getCommand = new Command(GetCommandHandler));

        private async void GetCommandHandler(object obj)
        {
            var res = await _usersService.GetUser(_user.Id);
            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                User = res.Data;
            }

            if (res.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                await NavigationService.NavigateAsync(nameof(LoginView));
            }
        }

        private ICommand _postCommand;
        public ICommand Post =>
            _postCommand ?? (_postCommand = new Command(PostCommandHandler));

        private async void PostCommandHandler(object obj)
        {
            var res = await _usersService.PostUsers(_user);
            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                User = new UserModel();
                await _dialogs.AlertAsync("Post");
            }
        }

        private ICommand _deleteCommand;
        public ICommand Delete =>
            _deleteCommand ?? (_deleteCommand = new Command(DeleteCommandHandler));

        private async void DeleteCommandHandler(object obj)
        {
            var res = await _usersService.DeleteUsers(_user.Id);
            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                User = new UserModel();
                await _dialogs.AlertAsync("Deleted");
            }
        }

        private ICommand _putCommand;
        public ICommand Put =>
            _putCommand ?? (_putCommand = new Command(PutCommandHandler));

        private async void PutCommandHandler(object obj)
        {
            var res = await _usersService.PutUsers(_user.Id, _user);
            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                await _dialogs.AlertAsync("Put");
            }
        }

        private ICommand _patchedCommand;
        public ICommand Patched =>
            _patchedCommand ?? (_patchedCommand = new Command(PatchedCommandHandler));

        private async void PatchedCommandHandler(object obj)
        {
            var res = await _usersService.PatchUsers(
                _user.Id, new { Name = _user.Name});
            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                await _dialogs.AlertAsync("Patched");
            }
        }

    }
}