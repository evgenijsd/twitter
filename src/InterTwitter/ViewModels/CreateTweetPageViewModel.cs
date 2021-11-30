using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.ViewModels
{
    public class CreateTweetPageViewModel : BaseViewModel
    {
        public CreateTweetPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}