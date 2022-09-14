﻿using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class LineSpacingLabel : Label
    {
        #region -- Public properties --

        public static readonly BindableProperty LineSpacingProperty = BindableProperty.Create(
            propertyName: nameof(LineSpacing),
            returnType: typeof(float),
            declaringType: typeof(LineSpacingLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public float LineSpacing
        {
            get => (float)GetValue(LineSpacingProperty);
            set => SetValue(LineSpacingProperty, value);
        }

        #endregion
    }
}
