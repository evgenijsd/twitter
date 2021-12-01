using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CircleProgressBar : ContentView
    {
        private SKCanvasView _canvasView;

        public CircleProgressBar()
        {
            _canvasView = new SKCanvasView();
            _canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = _canvasView;
        }

        #region -- Public properties --

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            propertyName: nameof(Value),
            returnType: typeof(int),
            declaringType: typeof(CircleProgressBar),
            defaultBindingMode: BindingMode.TwoWay);

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
            propertyName: nameof(Minimum),
            returnType: typeof(int),
            declaringType: typeof(CircleProgressBar),
            defaultBindingMode: BindingMode.TwoWay);

        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
            propertyName: nameof(Maximum),
            returnType: typeof(int),
            declaringType: typeof(CircleProgressBar),
            defaultValue: 100,
            defaultBindingMode: BindingMode.TwoWay);

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(CircleProgressBar),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(CircleProgressBar),
            defaultValue: Color.Black,
            defaultBindingMode: BindingMode.TwoWay);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(CircleProgressBar),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public static readonly BindableProperty LineWidthProperty = BindableProperty.Create(
            propertyName: nameof(LineWidth),
            returnType: typeof(int),
            declaringType: typeof(CircleProgressBar),
            defaultValue: 1,
            defaultBindingMode: BindingMode.TwoWay);

        public int LineWidth
        {
            get => (int)GetValue(LineWidthProperty);
            set => SetValue(LineWidthProperty, value);
        }

        public static readonly BindableProperty ProgressLineColorProperty = BindableProperty.Create(
            propertyName: nameof(ProgressLineColor),
            returnType: typeof(Color),
            declaringType: typeof(CircleProgressBar),
            defaultValue: Color.Blue,
            defaultBindingMode: BindingMode.TwoWay);

        public Color ProgressLineColor
        {
            get => (Color)GetValue(ProgressLineColorProperty);
            set => SetValue(ProgressLineColorProperty, value);
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

        public static readonly BindableProperty BackgroundProgressColorProperty = BindableProperty.Create(
            propertyName: nameof(BackgroundProgressColor),
            returnType: typeof(Color),
            declaringType: typeof(CircleProgressBar),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.TwoWay);

        public Color BackgroundProgressColor
        {
            get => (Color)GetValue(BackgroundProgressColorProperty);
            set => SetValue(BackgroundProgressColorProperty, value);
        }

        #endregion

        #region -- Private methods --

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);

            var lineWidthPercent = LineWidth * info.Height / 200;
            var radiusWithLine = center.Y - (lineWidthPercent / 2);
            var innerRadius = radiusWithLine - (lineWidthPercent / 2);

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = BackgroundLineColor.ToSKColor(),
                StrokeWidth = lineWidthPercent,
            };

            canvas.DrawCircle(center.X, center.Y, radiusWithLine, paint);

            using (SKPath path = new SKPath())
            {
                var range = Maximum - Minimum;
                var progress = Value - Minimum;
                var limit = (360 * progress / range) - 90;

                for (float angle = -90; angle < limit; angle += 0.5f)
                {
                    double radians = Math.PI * angle / 180;
                    float x = center.X + (radiusWithLine * (float)Math.Cos(radians));
                    float y = center.Y + (radiusWithLine * (float)Math.Sin(radians));

                    SKPoint point = new SKPoint(x, y);

                    if (angle == -90)
                    {
                        path.MoveTo(point);
                    }
                    else
                    {
                        path.LineTo(point);
                    }
                }

                SKPaint paint2 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = ProgressLineColor.ToSKColor(),
                    StrokeWidth = lineWidthPercent,
                };

                canvas.DrawPath(path, paint2);
            }

            paint.Style = SKPaintStyle.Fill;
            paint.Color = BackgroundProgressColor.ToSKColor();
            canvas.DrawCircle(center.X, center.Y, innerRadius, paint);

            SKPaint textPaint = new SKPaint
            {
                Color = TextColor.ToSKColor(),
            };

            var maxWidthText = (float)Math.Sqrt(2 * innerRadius * innerRadius);

            var bounds = new SKRect()
            {
                Size = new SKSize()
                {
                    Height = maxWidthText,
                    Width = maxWidthText,
                },
            };

            var textWidth = textPaint.MeasureText(Text, ref bounds);

            var textHeigth = maxWidthText * bounds.Size.Width / bounds.Size.Width;

            SKFont textFont = new SKFont
            {
                Typeface = SKTypeface.FromFamilyName(FontFamily),
                Size = maxWidthText * textPaint.TextSize / textWidth,
            };

            canvas.DrawText(Text, center.X, center.Y, textFont, textPaint);
        }

        #endregion

    }
}
