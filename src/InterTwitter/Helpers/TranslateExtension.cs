using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string BASE_NAME = "InterTwitter.Resources.Resource";
        private readonly CultureInfo ci;

        public TranslateExtension()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            ci = new CultureInfo("en-US");
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text != null)
            {
                ResourceManager resmgr = new ResourceManager(
                    BASE_NAME,
                    typeof(TranslateExtension).GetTypeInfo().Assembly);

                var translation = resmgr.GetString(Text, ci);

                if (translation == null)
                {
                    translation = Text;
                }

                return translation;
            }

            return string.Empty;
        }
    }
}
