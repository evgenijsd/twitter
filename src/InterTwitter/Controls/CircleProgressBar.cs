using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CircleProgressBar : SKCanvasView
    {
        public CircleProgressBar()
        {
            PaintSurface += OnCanvasViewPaintSurface;
        }

        #region -- Public properties --

        public static readonly BindableProperty RestrictionTextProperty = BindableProperty.Create(
            propertyName: nameof(RestrictionText),
            returnType: typeof(string),
            declaringType: typeof(CircleProgressBar),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string RestrictionText
        {
            get => (string)GetValue(RestrictionTextProperty);
            set => SetValue(RestrictionTextProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
            propertyName: nameof(MaxLength),
            returnType: typeof(string),
            declaringType: typeof(CircleProgressBar),
            defaultBindingMode: BindingMode.TwoWay);

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static readonly BindableProperty ExtraLengthProperty = BindableProperty.Create(
            propertyName: nameof(ExtraLength),
            returnType: typeof(string),
            declaringType: typeof(CircleProgressBar),
            defaultBindingMode: BindingMode.TwoWay);

        public int ExtraLength
        {
            get => (int)GetValue(ExtraLengthProperty);
            set => SetValue(ExtraLengthProperty, value);
        }

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(
            propertyName: nameof(ErrorColor),
            returnType: typeof(Color),
            declaringType: typeof(CircleProgressBar),
            defaultValue: Color.Red,
            defaultBindingMode: BindingMode.TwoWay);

        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
        }

        public static readonly BindableProperty ProgressingLineColorProperty = BindableProperty.Create(
            propertyName: nameof(ProgressingLineColor),
            returnType: typeof(Color),
            declaringType: typeof(CircleProgressBar),
            defaultValue: Color.Blue,
            defaultBindingMode: BindingMode.TwoWay);

        public Color ProgressingLineColor
        {
            get => (Color)GetValue(ProgressingLineColorProperty);
            set => SetValue(ProgressingLineColorProperty, value);
        }

        public static readonly BindableProperty BackgroundLineColorProperty = BindableProperty.Create(
            propertyName: nameof(BackgroundLineColor),
            returnType: typeof(Color),
            declaringType: typeof(CircleProgressBar),
            defaultValue: Color.Silver,
            defaultBindingMode: BindingMode.TwoWay);

        public Color BackgroundLineColor
        {
            get => (Color)GetValue(BackgroundLineColorProperty);
            set => SetValue(BackgroundLineColorProperty, value);
        }

        #endregion

        #region -- Private methods --

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 25
            };
            canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);

            paint.Style = SKPaintStyle.Fill;
            paint.Color = SKColors.Blue;
            canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
        }

        #endregion

    }
}
