using System;
using Prism.Mvvm;
using Prism.Navigation;
using Randevy.Infrastructure;
using Randevy.Infrastructure.Interfaces;

namespace Randevy.ViewModels
{
    public class BaseViewModel : BindableBase, IViewActionsHandler, INavigationAware, IDestructible
    {
        public readonly INavigationService NavigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region -- IViewActionsHandler implementation --

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        #endregion

        #region -- INavigationAware implementation --
        
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        #endregion

        #region -- IDestructible implementation --

        public virtual void Destroy()
        {

        }

        #endregion

        protected void AssertParametrExisting(NavigationParameters parameters, string parameter)
        {
            if (parameters.ContainsKey(parameter))
            {
                return;
            }
            else
            {
                throw new ArgumentException(parameter + " parameters doesn't setted");
            }
        }
    }
}