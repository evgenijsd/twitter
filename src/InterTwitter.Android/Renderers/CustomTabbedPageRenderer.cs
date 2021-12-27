using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.Platform.Android;
using Google.Android.Material.BottomNavigation;
using InterTwitter.Controls;
using InterTwitter.Droid;
using Google.Android.Material.Tabs;
using System.ComponentModel;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using InterTwitter.Views;
using InterTwitter.ViewModels;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace InterTwitter.Droid
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer, BottomNavigationView.IOnNavigationItemReselectedListener
    {
        private TabbedPage _tabbedPage;
        private BottomNavigationView _bottomNavigationView;
        private bool _isShiftModeSet;

        public CustomTabbedPageRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                _tabbedPage = e.NewElement as CustomTabbedPage;
                _bottomNavigationView = (GetChildAt(0) as Android.Widget.RelativeLayout).GetChildAt(1) as BottomNavigationView;
                _bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;
            }

        }

        public void OnNavigationItemReselected(IMenuItem item)
        {
            if (Element is CustomTabbedPage)
            {
                var mainTabPage = Element as CustomTabbedPage;
                _tabbedPage.CurrentPage.Navigation.PopToRootAsync();
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            try
            {
                if (!_isShiftModeSet)
                {
                    var children = GetAllChildViews(ViewGroup);

                    if (children.SingleOrDefault(x => x is BottomNavigationView) is BottomNavigationView bottomNav)
                    {
                        bottomNav.SetOnNavigationItemReselectedListener(this);
                        //bottomNav.SetShiftMode(false, false);
                        _isShiftModeSet = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error setting ShiftMode: {e}");
            }
        }

        private List<Android.Views.View> GetAllChildViews(Android.Views.View view)
        {
            if (!(view is ViewGroup group))
            {
                return new List<Android.Views.View> { view };
            }

            var result = new List<Android.Views.View>();

            for (int i = 0; i < group.ChildCount; i++)
            {
                var child = group.GetChildAt(i);

                var childList = new List<Android.Views.View> { child };
                childList.AddRange(GetAllChildViews(child));

                result.AddRange(childList);
            }

            return result.Distinct().ToList();
        }
    }
}