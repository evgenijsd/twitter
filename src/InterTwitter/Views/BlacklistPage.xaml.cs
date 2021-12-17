using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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