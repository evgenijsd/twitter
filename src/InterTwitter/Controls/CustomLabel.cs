using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomLabel : Label
    {
        public CustomLabel()
        {
            SizeChanged += OnSizeChanged;
        }

        #region -- Public properties --

        public static readonly BindableProperty IsSpanVisibleProperty = BindableProperty.Create(
             propertyName: nameof(IsSpanVisible),
             returnType: typeof(bool),
             declaringType: typeof(CustomLabel),
             defaultBindingMode: BindingMode.TwoWay);
        public bool IsSpanVisible
        {
            get => (bool)GetValue(IsSpanVisibleProperty);
            set => SetValue(IsSpanVisibleProperty, value);
        }

        #endregion

        #region  -- Private helpers --

        private void OnSizeChanged(object sender, EventArgs e)
        {
            var maxRowNumber = 5;
            var fontSize = (sender as Label).FontSize;
            var maxLabelHeight = fontSize * 1.166666666666669 * maxRowNumber;
            if ((sender as Label).Height > maxLabelHeight)
            {
                IsSpanVisible = true;
            }
        }

        #endregion

    }
}
