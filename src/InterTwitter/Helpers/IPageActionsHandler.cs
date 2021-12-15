using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Helpers
{
    public interface IPageActionsHandler
    {
        void OnAppearing();
        void OnDisappearing();
    }
}
