using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace InterTwitter.ViewModels
{
    public class MenuItemViewModel : BindableBase
    {
        public MenuItemViewModel()
        {
            TargetType = typeof(MenuItemViewModel);
        }

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private Type _targetType;
        public Type TargetType
        {
            get => _targetType;
            set => SetProperty(ref _targetType, value);
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private Color _textColor;
        public Color TextColor
        {
            get => _textColor;
            set => SetProperty(ref _textColor, value);
        }
    }
}
