using InterTwitter.ViewModels;

namespace InterTwitter.Views
{
    public partial class SearchPage : BaseContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        #region -- Overrides --

        protected override bool OnBackButtonPressed()
        {
            bool isCannotGoBack = false;

            if (BindingContext is SearchPageViewModel viewModel)
            {
                if (viewModel.TweetsSearchState == Enums.ESearchState.Active)
                {
                    viewModel.ResetSearchData();
                    isCannotGoBack = true;
                }
            }

            return isCannotGoBack;
        }

        #endregion
    }
}