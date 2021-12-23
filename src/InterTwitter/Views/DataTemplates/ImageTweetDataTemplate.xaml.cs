using DLToolkit.Forms.Controls;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace InterTwitter.Views.DataTemplates
{
    public partial class ImageTweetDataTemplate : DataTemplate
    {
        public ImageTweetDataTemplate()
        {
            InitializeComponent();
        }

        private void FlowListView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var flow = sender as FlowListView;

            var first = ((List<string>)flow.FlowItemsSource).LastOrDefault();

            flow.FlowScrollTo(first, ScrollToPosition.Start, false);
        }
    }
}