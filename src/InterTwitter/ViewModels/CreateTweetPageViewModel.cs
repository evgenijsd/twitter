using InterTwitter.Enums;
using InterTwitter.Services.PermissionsService;
using MapNotepad.Helpers;
using Prism.Navigation;
using Prism.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class CreateTweetPageViewModel : BaseViewModel
    {
        private int value;

        private IPageDialogService _pageDialogService;

        private IPermissionsService _permissionsService;

        public CreateTweetPageViewModel(
            INavigationService navigationService,
            IPermissionsService permissionsService,
            IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. A, neque, metus ipsum fermentum morbi at.";
            value = _text.Length;

            _permissionsService = permissionsService;
            _pageDialogService = pageDialogService;

            _listAttachedMedia = new ObservableCollection<MiniCardViewModel>();
        }

        #region -- Public properties --

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _circleProgressBarText;
        public string CircleProgressBarText
        {
            get => _circleProgressBarText;
            set => SetProperty(ref _circleProgressBarText, value);
        }

        private Color _circleProgressBarTextColor;
        public Color CircleProgressBarTextColor
        {
            get => _circleProgressBarTextColor;
            set => SetProperty(ref _circleProgressBarTextColor, value);
        }

        private float _circleProgressBarFontScale;
        public float CircleProgressBarFontScale
        {
            get => _circleProgressBarFontScale;
            set => SetProperty(ref _circleProgressBarFontScale, value);
        }

        private SKTypefaceStyle _circleProgressBarFontAttributes;
        public SKTypefaceStyle CircleProgressBarFontAttributes
        {
            get => _circleProgressBarFontAttributes;
            set => SetProperty(ref _circleProgressBarFontAttributes, value);
        }

        private int _circleProgressBarvalue;
        public int CircleProgressBarValue
        {
            get => _circleProgressBarvalue;
            set => SetProperty(ref _circleProgressBarvalue, value);
        }

        private Color _circleProgressBarProgressLineColor = Color.Blue;
        public Color CircleProgressBarProgressLineColor
        {
            get => _circleProgressBarProgressLineColor;
            set => SetProperty(ref _circleProgressBarProgressLineColor, value);
        }

        private bool _canUseButtonUploadPhotos = true;
        public bool CanUseButtonUploadPhotos
        {
            get => _canUseButtonUploadPhotos;
            set => SetProperty(ref _canUseButtonUploadPhotos, value);
        }

        private bool _canUseButtonUploadGif = true;
        public bool CanUseButtonUploadGif
        {
            get => _canUseButtonUploadGif;
            set => SetProperty(ref _canUseButtonUploadGif, value);
        }

        private bool _canUseButtonUploadVideo = true;
        public bool CanUseButtonUploadVideo
        {
            get => _canUseButtonUploadVideo;
            set => SetProperty(ref _canUseButtonUploadVideo, value);
        }

        private bool _canUseButtonPost = true;
        public bool CanUseButtonPost
        {
            get => _canUseButtonPost;
            set => SetProperty(ref _canUseButtonPost, value);
        }

        private ETypeAttachedMedia _typeAttachedMedia;
        public ETypeAttachedMedia TypeAttachedMedia
        {
            get => _typeAttachedMedia;
            set => SetProperty(ref _typeAttachedMedia, value);
        }

        private ObservableCollection<MiniCardViewModel> _listAttachedMedia;
        public ObservableCollection<MiniCardViewModel> ListAttachedMedia
        {
            get => _listAttachedMedia;
            set => SetProperty(ref _listAttachedMedia, value);
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand = SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        private ICommand _postTweetCommand;
        public ICommand PostTweetCommand => _postTweetCommand = SingleExecutionCommand.FromFunc(OnPostTweetCommandAsync);

        private ICommand _deleteAttachedPhotoCommand;
        public ICommand DeleteAttachedPhotoCommand => _deleteAttachedPhotoCommand = SingleExecutionCommand.FromFunc(OnDeleteAttachedPhotoCommandAsync);

        private ICommand _deleteAttachedGifCommand;
        public ICommand DeleteAttachedGifCommand => _deleteAttachedGifCommand = SingleExecutionCommand.FromFunc(OnDeleteAttachedGifAsync);

        private ICommand _deleteAttachedVideoCommand;
        public ICommand DeleteAttachedVideoCommand => _deleteAttachedVideoCommand = SingleExecutionCommand.FromFunc(OnDeleteAttachedVideoAsync);

        private ICommand _addPhotoCommand;
        public ICommand AddPhotoCommand => _addPhotoCommand = SingleExecutionCommand.FromFunc(OnAddPhotoAsync);

        private ICommand _addGifCommand;
        public ICommand AddGifCommand => _addGifCommand = SingleExecutionCommand.FromFunc(OnAddGifAsync);

        private ICommand _addVideoCommand;
        public ICommand AddVideoCommand => _addVideoCommand = SingleExecutionCommand.FromFunc(OnAddVideoAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Text):
                    CanUseButtonPost = Text.Length < 250;
                    Counter();
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private async Task OnGoBackCommandAsync()
        {
            var confirm = await _pageDialogService.DisplayAlertAsync("Confirm", "Confirm?", "Ok", "Cancel");

            if (confirm)
            {
                await _pageDialogService.DisplayAlertAsync("Exit", "Confirm", "Ok");
            }
        }

        private async Task OnPostTweetCommandAsync()
        {
        }

        private async Task OnDeleteAttachedPhotoCommandAsync(object obj)
        {
            var item = obj as MiniCardViewModel;

            ListAttachedMedia.Remove(item);

            CanUseButtonUploadPhotos = ListAttachedMedia.Count < 6;

            if (ListAttachedMedia.Count == 0)
            {
                CanUseButtonUploadGif = true;
                CanUseButtonUploadVideo = true;

                TypeAttachedMedia = ETypeAttachedMedia.None;
            }
        }

        private async Task OnDeleteAttachedGifAsync(object obj)
        {
            var item = obj as MiniCardViewModel;

            ListAttachedMedia.Clear();

            CanUseButtonUploadPhotos = true;
            CanUseButtonUploadGif = true;
            CanUseButtonUploadVideo = true;

            TypeAttachedMedia = ETypeAttachedMedia.None;
        }

        private async Task OnDeleteAttachedVideoAsync(object obj)
        {
            var item = obj as MiniCardViewModel;

            ListAttachedMedia.Clear();

            CanUseButtonUploadPhotos = true;
            CanUseButtonUploadGif = true;
            CanUseButtonUploadVideo = true;

            TypeAttachedMedia = ETypeAttachedMedia.None;
        }

        private async Task OnAddPhotoAsync()
        {
            var canUseStorage = await _permissionsService.RequestAsync<Permissions.StorageRead>() == Xamarin.Essentials.PermissionStatus.Granted;

            if (canUseStorage)
            {
                var photo = await MediaPicker.PickPhotoAsync();

                ListAttachedMedia.Add(new MiniCardViewModel()
                {
                    PathImage = photo.FullPath,
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = DeleteAttachedPhotoCommand,
                });

                CanUseButtonUploadPhotos = ListAttachedMedia.Count < 6;
                CanUseButtonUploadGif = false;
                CanUseButtonUploadVideo = false;

                TypeAttachedMedia = ETypeAttachedMedia.Photos;
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Error", "This app needs access to photos gallery for picking photos and videos.", "Ok");
            }
        }

        private async Task OnAddGifAsync()
        {
            var canUseStorage = await _permissionsService.RequestAsync<Permissions.StorageRead>() == Xamarin.Essentials.PermissionStatus.Granted;

            if (canUseStorage)
            {
                var photo = await MediaPicker.PickPhotoAsync();

                ListAttachedMedia.Add(new MiniCardViewModel()
                {
                    PathImage = photo.FullPath,
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = DeleteAttachedGifCommand,
                });

                CanUseButtonUploadPhotos = false;
                CanUseButtonUploadGif = false;
                CanUseButtonUploadVideo = false;

                TypeAttachedMedia = ETypeAttachedMedia.Gif;
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Error", "This app needs access to photos gallery for picking photos and videos.", "Ok");
            }
        }

        private async Task OnAddVideoAsync()
        {
            var canUseStorage = await _permissionsService.RequestAsync<Permissions.StorageRead>() == Xamarin.Essentials.PermissionStatus.Granted;

            if (canUseStorage)
            {
                var photo = await MediaPicker.PickVideoAsync();

                ListAttachedMedia.Add(new MiniCardViewModel()
                {
                    PathImage = photo.FullPath,
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = DeleteAttachedVideoCommand,
                });

                CanUseButtonUploadPhotos = false;
                CanUseButtonUploadGif = false;
                CanUseButtonUploadVideo = false;

                TypeAttachedMedia = ETypeAttachedMedia.Video;
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Error", "This app needs access to photos gallery for picking photos and videos.", "Ok");
            }
        }

        private void Counter()
        {
            value = Text.Length;

            if (value > 250)
            {
                CircleProgressBarTextColor = Color.Red;
                CircleProgressBarFontScale = 0.7f;
                CircleProgressBarProgressLineColor = Color.Red;

                CircleProgressBarText = (250 - value).ToString();
            }
            else
            {
                CircleProgressBarText = " ";
                CircleProgressBarProgressLineColor = Color.Blue;
            }

            if (value == 300)
            {
                CircleProgressBarText = ":D";
                CircleProgressBarFontScale = 1;
                CircleProgressBarFontAttributes = SKTypefaceStyle.Bold;
            }

            CircleProgressBarValue = value;
        }

        #endregion
    }
}