using InterTwitter.Controls;
using InterTwitter.iOS.Renderers;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace InterTwitter.iOS.Renderers
{
    class CustomTabbedPageRenderer : TabbedRenderer
    {
        private TabbedPage _tabbedPage;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _tabbedPage = (TabbedPage)e.NewElement;
            }
            else
            {
                _tabbedPage = (TabbedPage)e.OldElement;
            }

            try
            {
                var tabbarController = (UITabBarController)this.ViewController;
                if (null != tabbarController)
                {
                    tabbarController.ViewControllerSelected += OnTabbarControllerItemSelected;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private async void OnTabbarControllerItemSelected(object sender, UITabBarSelectionEventArgs eventArgs)
        {
            if (_tabbedPage?.CurrentPage?.Navigation != null && _tabbedPage.CurrentPage.Navigation.NavigationStack.Count > 0)
            {
                await _tabbedPage.CurrentPage.Navigation.PopToRootAsync();
            }

        }
    }
}