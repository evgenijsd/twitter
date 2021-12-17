using InterTwitter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Controls
{
    public partial class TestLabel : ContentView
    {
        public TestLabel()
        {
        }

        public static readonly BindableProperty ModeProperty = BindableProperty.Create(
            propertyName: nameof(Mode),
            returnType: typeof(EStateMode),
            declaringType: typeof(TestLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public EStateMode Mode
        {
            get => (EStateMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
    }
}