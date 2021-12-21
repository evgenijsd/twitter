using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace InterTwitter.ViewModels
{
    public class AlertViewModel : BindableBase, IDialogAware
    {
        public AlertViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
        }

        #region -- Public properties --

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand CloseCommand { get; }

        public event Action<IDialogParameters> RequestClose;

        #endregion

        #region -- Interface implementation --

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.TryGetValue(Constants.Navigation.MESSAGE, out string message))
            {
                Message = message;
            }
        }

        #endregion
    }
}
