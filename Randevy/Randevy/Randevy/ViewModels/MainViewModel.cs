using Prism.Navigation;
using System;
using System.Windows.Input;
using Randevy.Views;
using Xamarin.Forms;

namespace Randevy.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        

        private ICommand _onTapUsers;
        public ICommand OnTapUsers =>
            _onTapUsers ?? (_onTapUsers = new Command(async () =>
                await NavigationService.NavigateAsync(nameof(UsersView))));


        private ICommand _onTapUser;
        public ICommand OnTapUser =>
            _onTapUser ?? (_onTapUser = new Command(async () =>
                await NavigationService.NavigateAsync(nameof(UserView))));


    }
}