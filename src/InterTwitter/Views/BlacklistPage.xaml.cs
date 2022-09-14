using System;

namespace InterTwitter.Views
{
    public partial class BlacklistPage : BaseContentPage
    {
        public BlacklistPage()
        {
            InitializeComponent();
        }

        public void OnTapEventHandler(object sender, EventArgs e)
        {
            blacklist.SelectedItem = null;
        }
    }
}