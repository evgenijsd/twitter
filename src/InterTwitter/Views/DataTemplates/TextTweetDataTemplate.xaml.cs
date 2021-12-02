using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views.DataTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextTweetDataTemplate : DataTemplate
    {
        public TextTweetDataTemplate()
        {
            InitializeComponent();
        }
    }
}