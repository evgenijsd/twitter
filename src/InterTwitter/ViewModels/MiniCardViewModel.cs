using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class MiniCardViewModel : BindableBase
    {
        private string _pathImage;
        public string PathImage
        {
            get => _pathImage;
            set => SetProperty(ref _pathImage, value);
        }

        private string _pathActionImage;
        public string PathActionImage
        {
            get => _pathActionImage;
            set => SetProperty(ref _pathActionImage, value);
        }

        private ICommand _actionCommand;
        public ICommand ActionCommand
        {
            get => _actionCommand;
            set => SetProperty(ref _actionCommand, value);
        }
    }
}
