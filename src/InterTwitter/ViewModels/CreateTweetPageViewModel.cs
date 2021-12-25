using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Resources.Strings;
using InterTwitter.Services;
using InterTwitter.Services.Hashtag;
using InterTwitter.Services.PermissionsService;
using InterTwitter.Services.VideoService;
using InterTwitter.Views;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoTrimmer.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class CreateTweetPageViewModel : BaseViewModel
    {
        private IPageDialogService _pageDialogService;

        private IPermissionsService _permissionsService;

        private IVideoService _videoService;

        private ITweetService _tweetService;

        private IAuthorizationService _authorizationService;

        private IRegistrationService _registrationService;

        private IDialogService _dialogService;

        private int _userId;

        public CreateTweetPageViewModel(
            INavigationService navigationService,
            IPermissionsService permissionsService,
            IPageDialogService pageDialogService,
            IVideoService videoService,
            ITweetService tweetService,
            IAuthorizationService authorizationService,
            IRegistrationService registrationService,
            IDialogService dialogService)
            : base(navigationService)
        {
            _permissionsService = permissionsService;
            _pageDialogService = pageDialogService;
            _videoService = videoService;
            _tweetService = tweetService;
            _authorizationService = authorizationService;
            _registrationService = registrationService;
            _dialogService = dialogService;

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

        private bool _canUseButtonPost;
        public bool CanUseButtonPost
        {
            get => _canUseButtonPost;
            set => SetProperty(ref _canUseButtonPost, value);
        }

        private bool _isRunnigActivityIndicator;
        public bool IsRunnigActivityIndicator
        {
            get => _isRunnigActivityIndicator;
            set => SetProperty(ref _isRunnigActivityIndicator, value);
        }

        private EAttachedMediaType _typeAttachedMedia = EAttachedMediaType.None;
        public EAttachedMediaType TypeAttachedMedia
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

        private string _avatarPath = "pic_profile_small.png";
        public string AvatarPath
        {
            get => _avatarPath;
            set => SetProperty(ref _avatarPath, value);
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

        #region -- IInitializeAsync implementation --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            _userId = _authorizationService.UserId;
            var userModel = await _registrationService.GetByIdAsync(_userId);

            if (userModel.IsSuccess)
            {
                AvatarPath = userModel.Result.AvatarPath;
            }
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Text):
                    CanUseButtonPost = canPostTweet();
                    Counter();
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private async Task OnGoBackCommandAsync()
        {
            if ((!string.IsNullOrEmpty(Text) && Text.Length > 0) || ListAttachedMedia.Count > 0)
            {
                var confirm = await _pageDialogService.DisplayAlertAsync(Strings.Confirm, Strings.ConfirmComeBack, Strings.Ok, Strings.Cancel);

                if (confirm)
                {
                    await _navigationService.GoBackAsync();
                }
            }
            else
            {
                await _navigationService.GoBackAsync();
            }
        }

        private async Task OnPostTweetCommandAsync()
        {
            var list = new List<string>();

            foreach (var card in _listAttachedMedia)
            {
                list.Add(card.PathFile);
            }

            var newTweet = new TweetModel()
            {
                UserId = _userId,
                Text = Text,
                Media = _typeAttachedMedia,
                MediaPaths = list,
                CreationTime = DateTime.Now,
            };

            await _tweetService.AddTweetAsync(newTweet);
            await _navigationService.GoBackAsync();
        }

        private async Task OnDeleteAttachedPhotoCommandAsync(object obj)
        {
            var item = obj as MiniCardViewModel;

            ListAttachedMedia.Remove(item);

            CanUseButtonUploadPhotos = ListAttachedMedia.Count < Constants.Limits.MAX_COUNT_ATTACHED_PHOTOS;

            if (ListAttachedMedia.Count == 0)
            {
                clearAttachedMedia();
            }
            else
            {
                CanUseButtonPost = canPostTweet();
            }
        }

        private async Task OnDeleteAttachedGifAsync(object obj)
        {
            clearAttachedMedia();
        }

        private async Task OnDeleteAttachedVideoAsync(object obj)
        {
            clearAttachedMedia();
        }

        private async Task OnAddPhotoAsync()
        {
            try
            {
                var canUseStorage = await _permissionsService.RequestAsync<Permissions.StorageRead>() == Xamarin.Essentials.PermissionStatus.Granted;

                if (canUseStorage)
                {
                    var openFile = await MediaPicker.PickPhotoAsync();

                    if (openFile.ContentType != "image/gif")
                    {
                        FileInfo fileInf = new FileInfo(openFile.FullPath);

                        if (fileInf.Exists)
                        {
                            if (fileInf.Length <= Constants.Limits.MAX_SIZE_ATTACHED_PHOTO)
                            {
                                ListAttachedMedia.Add(new MiniCardViewModel()
                                {
                                    PathFile = openFile.FullPath,
                                    PathActionImage = "ic_clear_filled_blue.png",
                                    ActionCommand = DeleteAttachedPhotoCommand,
                                });

                                CanUseButtonUploadPhotos = ListAttachedMedia.Count < Constants.Limits.MAX_COUNT_ATTACHED_PHOTOS;
                                CanUseButtonUploadGif = false;
                                CanUseButtonUploadVideo = false;

                                TypeAttachedMedia = EAttachedMediaType.Photos;

                                CanUseButtonPost = canPostTweet();
                            }
                            else
                            {
                                var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertLimitSizePhoto } };
                                await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                            }
                        }
                        else
                        {
                            var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertFileNotExist } };
                            await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                        }
                    }
                    else
                    {
                        var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertOnlyPicture } };
                        await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                    }
                }
                else
                {
                    var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertNeedAccessPhotosGallery } };
                    await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                }
            }
            catch (Exception e)
            {
            }
        }

        private async Task OnAddGifAsync()
        {
            try
            {
                var canUseStorage = await _permissionsService.RequestAsync<Permissions.StorageRead>() == Xamarin.Essentials.PermissionStatus.Granted;

                if (canUseStorage)
                {
                    var openFile = await MediaPicker.PickPhotoAsync();

                    if (openFile.ContentType == "image/gif")
                    {
                        FileInfo fileInf = new FileInfo(openFile.FullPath);

                        if (fileInf.Exists)
                        {
                            if (fileInf.Length <= Constants.Limits.MAX_SIZE_ATTACHED_PHOTO)
                            {
                                ListAttachedMedia.Add(new MiniCardViewModel()
                                {
                                    PathFile = openFile.FullPath,
                                    PathActionImage = "ic_clear_filled_blue.png",
                                    ActionCommand = DeleteAttachedGifCommand,
                                });

                                CanUseButtonUploadPhotos = false;
                                CanUseButtonUploadGif = false;
                                CanUseButtonUploadVideo = false;

                                TypeAttachedMedia = EAttachedMediaType.Gif;

                                CanUseButtonPost = canPostTweet();
                            }
                            else
                            {
                                var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertLimitSizeGif } };
                                await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                            }
                        }
                        else
                        {
                            var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertFileNotExist } };
                            await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                        }
                    }
                    else
                    {
                        var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertOnlyGif } };
                        await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                    }
                }
                else
                {
                    var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertNeedAccessPhotosGallery } };
                    await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                }
            }
            catch (Exception e)
            {
            }
        }

        private async Task OnAddVideoAsync()
        {
            try
            {
                var canUseStorage = await _permissionsService.RequestAsync<Permissions.StorageRead>() == Xamarin.Essentials.PermissionStatus.Granted
                                && await _permissionsService.RequestAsync<Permissions.StorageWrite>() == Xamarin.Essentials.PermissionStatus.Granted;

                if (canUseStorage)
                {
                    var openFile = await MediaPicker.PickVideoAsync();
                    var pathFile = openFile.FullPath;

                    FileInfo fileInf = new FileInfo(pathFile);

                    if (fileInf.Exists)
                    {
                        if (fileInf.Length <= Constants.Limits.MAX_SIZE_ATTACHED_VIDEO)
                        {
                            var videoLenght = _videoService.TryVideoLength(pathFile);

                            if (videoLenght > Constants.Limits.MAX_LENGTH_VIDEO)
                            {
                                IsRunnigActivityIndicator = true;

                                string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + (Device.RuntimePlatform == Device.iOS ? ".MOV" : ".mp4");
                                string outputPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                                if (await VideoTrimmerService.Instance.TrimAsync(0, Constants.Limits.MAX_LENGTH_VIDEO * 1000, pathFile, outputPath))
                                {
                                    pathFile = outputPath;
                                    IsRunnigActivityIndicator = false;
                                }
                                else
                                {
                                    var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertVideoTrimmingFailed } };
                                    await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                                    IsRunnigActivityIndicator = false;
                                }
                            }

                            string fileNameThumb = DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
                            var pathThumb = Path.Combine(FileSystem.AppDataDirectory, fileNameThumb);

                            using (var source = new System.IO.FileStream(pathThumb, System.IO.FileMode.OpenOrCreate))
                            {
                                Stream image = _videoService.TryGenerateThumbImage(pathFile, (long)(videoLenght / 2));
                                image.CopyTo(source);
                            }

                            ListAttachedMedia.Add(new MiniCardViewModel()
                            {
                                PathFile = pathThumb,
                            });

                            ListAttachedMedia.Add(new MiniCardViewModel()
                            {
                                PathFile = pathFile,
                            });

                            CanUseButtonUploadPhotos = false;
                            CanUseButtonUploadGif = false;
                            CanUseButtonUploadVideo = false;

                            TypeAttachedMedia = EAttachedMediaType.Video;

                            CanUseButtonPost = canPostTweet();
                        }
                        else
                        {
                            var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertLimitSizeVideo } };
                            await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                        }
                    }
                    else
                    {
                        var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertFileNotExist } };
                        await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                    }
                }
                else
                {
                    var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Strings.AlertNeedAccessPhotosGallery } };
                    await _dialogService.ShowDialogAsync(nameof(AlertView), parametrs);
                }
            }
            catch (Exception e)
            {
            }
        }

        private void clearAttachedMedia()
        {
            ListAttachedMedia.Clear();

            CanUseButtonUploadPhotos = true;
            CanUseButtonUploadGif = true;
            CanUseButtonUploadVideo = true;

            TypeAttachedMedia = EAttachedMediaType.None;

            CanUseButtonPost = canPostTweet();
        }

        private bool canPostTweet()
        {
            bool result;

            if (!string.IsNullOrEmpty(Text))
            {
                result = (ListAttachedMedia.Count > 0 || Text.Length > 0) && Text.Length <= Constants.Limits.MAX_LENGTH_TEXT;
            }
            else
            {
                result = ListAttachedMedia.Count > 0;
            }

            return result;
        }

        private void Counter()
        {
            var value = Text.Length;

            if (value > Constants.Limits.MAX_LENGTH_TEXT)
            {
                CircleProgressBarTextColor = Color.Red;
                CircleProgressBarFontScale = 0.7f;
                CircleProgressBarProgressLineColor = Color.Red;

                CircleProgressBarText = (Constants.Limits.MAX_LENGTH_TEXT - value).ToString();
            }
            else
            {
                CircleProgressBarText = " ";
                CircleProgressBarProgressLineColor = Color.Blue;
            }

            if (value == Constants.Limits.MAX_LENGTH_TEXT + 50)
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