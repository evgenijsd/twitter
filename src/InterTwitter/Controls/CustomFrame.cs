using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomFrame : Frame
    {
        public static BindableProperty ElevationProperty = BindableProperty.Create(
            propertyName: nameof(Elevation),
            returnType: typeof(float),
            declaringType: typeof(CustomFrame),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: 4.0f);

        public float Elevation
        {
            get => (float)GetValue(ElevationProperty);
            set => SetValue(ElevationProperty, value);
        }
    }
}
