using Randevy.Infrastructure;
using Randevy.Infrastructure.Interfaces;
using Xamarin.Forms;

namespace Randevy.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            //NavigationPage.SetHasNavigationBar(this, false);
        }

        #region -- Overrides --

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IViewActionsHandler actionsHandler = BindingContext as IViewActionsHandler;
            actionsHandler?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IViewActionsHandler actionsHandler = BindingContext as IViewActionsHandler;
            actionsHandler?.OnDisappearing();
        }

        #endregion
    }
}