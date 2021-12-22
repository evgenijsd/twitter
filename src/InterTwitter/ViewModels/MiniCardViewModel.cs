using Prism.Mvvm;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class MiniCardViewModel : BindableBase
    {
        private string _pathFile;
        public string PathFile
        {
            get => _pathFile;
            set => SetProperty(ref _pathFile, value);
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
