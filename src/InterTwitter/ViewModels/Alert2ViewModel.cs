using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace InterTwitter.ViewModels
{
    public class Alert2ViewModel : BindableBase, IDialogAware
    {
        public Alert2ViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
            AcceptCommand = new DelegateCommand(() => RequestClose(new DialogParameters() { { "Accept", true } }));
            DeclineCommand = new DelegateCommand(() => RequestClose(new DialogParameters() { { "Accept", false } }));
        }

        #region -- Public properties --

        public bool IsMessageVisible => !string.IsNullOrEmpty(Message);

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                SetProperty(ref _message, value);
                RaisePropertyChanged(nameof(IsMessageVisible));
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _okButtonText;
        public string OkButtonText
        {
            get => _okButtonText;
            set => SetProperty(ref _okButtonText, value);
        }

        private string _cancelButtonText;
        public string CancelButtonText
        {
            get => _cancelButtonText;
            set => SetProperty(ref _cancelButtonText, value);
        }

        public DelegateCommand CloseCommand { get; }
        public DelegateCommand AcceptCommand { get; }
        public DelegateCommand DeclineCommand { get; }

        public event Action<IDialogParameters> RequestClose;

        #endregion

        #region -- Interface implementation --

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("message"))
            {
                Message = parameters.GetValue<string>("message");
            }

            if (parameters.ContainsKey("title"))
            {
                Title = parameters.GetValue<string>("title");
            }

            if (parameters.ContainsKey("okButtonText"))
            {
                OkButtonText = parameters.GetValue<string>("okButtonText");
            }

            if (parameters.ContainsKey("cancelButtonText"))
            {
                CancelButtonText = parameters.GetValue<string>("cancelButtonText");
            }
        }

        #endregion
    }
}
