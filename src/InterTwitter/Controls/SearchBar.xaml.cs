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

            SearchState = ESearchState.NotActive;
        }

        #region --- Public properties ---

        public static BindableProperty SearchStateProperty = BindableProperty.Create(
            propertyName: nameof(SearchState),
            returnType: typeof(ESearchState),
            declaringType: typeof(SearchBar),
            defaultValue: ESearchState.Active,
            defaultBindingMode: BindingMode.TwoWay);

        public ESearchState SearchState
        {
            get => (ESearchState)GetValue(SearchStateProperty);
            private set => SetValue(SearchStateProperty, value);
        }

        public static BindableProperty QueryStringProperty = BindableProperty.Create(
            propertyName: nameof(QueryString),
            returnType: typeof(string),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public string QueryString
        {
            get => (string)GetValue(QueryStringProperty);
            set => SetValue(QueryStringProperty, value);
        }

        public static BindableProperty AvatarIconSourceProperty = BindableProperty.Create(
            propertyName: nameof(AvatarIconSource),
            returnType: typeof(ImageSource),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ImageSource AvatarIconSource
        {
            get => (ImageSource)GetValue(AvatarIconSourceProperty);
            set => SetValue(AvatarIconSourceProperty, value);
        }

        public static BindableProperty AvatarIconTapCommandProperty = BindableProperty.Create(
            propertyName: nameof(AvatarIconTapCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand AvatarIconTapCommand
        {
            get => (ICommand)GetValue(AvatarIconTapCommandProperty);
            set => SetValue(AvatarIconTapCommandProperty, value);
        }

        public static BindableProperty BackIconTapCommandProperty = BindableProperty.Create(
            propertyName: nameof(BackIconTapCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand BackIconTapCommand
        {
            get => (ICommand)GetValue(BackIconTapCommandProperty);
            set => SetValue(BackIconTapCommandProperty, value);
        }

        public static BindableProperty PressOkOnKeyboardCommandProperty = BindableProperty.Create(
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