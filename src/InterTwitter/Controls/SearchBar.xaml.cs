﻿using InterTwitter.Enums;
using InterTwitter.Helpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class SearchBar : ContentView
    {
        public SearchBar()
        {
            InitializeComponent();

            SearchStatus = ESearchStatus.NotActive;
        }

        #region --- Public properties ---

        public static readonly BindableProperty SearchStatusProperty = BindableProperty.Create(
            propertyName: nameof(SearchStatus),
            returnType: typeof(ESearchStatus),
            declaringType: typeof(SearchBar),
            defaultValue: ESearchStatus.Active,
            defaultBindingMode: BindingMode.TwoWay);

        public ESearchStatus SearchStatus
        {
            get => (ESearchStatus)GetValue(SearchStatusProperty);
            private set => SetValue(SearchStatusProperty, value);
        }

        public static readonly BindableProperty QueryStringProperty = BindableProperty.Create(
            propertyName: nameof(QueryString),
            returnType: typeof(string),
            defaultValue: string.Empty,
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public string QueryString
        {
            get => (string)GetValue(QueryStringProperty);
            set => SetValue(QueryStringProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
            propertyName: nameof(MaxLength),
            returnType: typeof(int),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static readonly BindableProperty AvatarIconSourceProperty = BindableProperty.Create(
            propertyName: nameof(AvatarIconSource),
            returnType: typeof(ImageSource),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ImageSource AvatarIconSource
        {
            get => (ImageSource)GetValue(AvatarIconSourceProperty);
            set => SetValue(AvatarIconSourceProperty, value);
        }

        public static readonly BindableProperty AvatarIconTapCommandProperty = BindableProperty.Create(
            propertyName: nameof(AvatarIconTapCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand AvatarIconTapCommand
        {
            get => (ICommand)GetValue(AvatarIconTapCommandProperty);
            set => SetValue(AvatarIconTapCommandProperty, value);
        }

        public static readonly BindableProperty BackIconTapCommandProperty = BindableProperty.Create(
            propertyName: nameof(BackIconTapCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand BackIconTapCommand
        {
            get => (ICommand)GetValue(BackIconTapCommandProperty);
            set => SetValue(BackIconTapCommandProperty, value);
        }

        public static readonly BindableProperty PressOkOnKeyboardCommandProperty = BindableProperty.Create(
            propertyName: nameof(PressOkOnKeyboardCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand PressOkOnKeyboardCommand
        {
            get => (ICommand)GetValue(PressOkOnKeyboardCommandProperty);
            set => SetValue(PressOkOnKeyboardCommandProperty, value);
        }

        private ICommand _searchFrameTapCommand;
        public ICommand SearchEdgesTapCommand => _searchFrameTapCommand ??= SingleExecutionCommand.FromFunc(OnSearchEdgesTap);

        #endregion

        #region --- Private helpers ---

        private Task OnSearchEdgesTap()
        {
            searchEntry.Focus();

            return Task.CompletedTask;
        }

        #endregion
    }
}