using InterTwitter.Enums;
using InterTwitter.Models.NotificationViewModel;
using Xamarin.Forms;

namespace InterTwitter.Helpers
{
    public class NotificationDataTemplateSelector : DataTemplateSelector
    {
        #region -- Public properties

        public DataTemplate VideoNotificationDataTemplate { get; set; }

        public DataTemplate ImageNotificationDataTemplate { get; set; }

        public DataTemplate TextNotificationDataTemplate { get; set; }

        #endregion

        #region -- Overrides --

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate dataTemplate = null;

            var tweet = item as BaseNotificationViewModel;
            switch (tweet.Media)
            {
                case EAttachedMediaType.Photos:
                case EAttachedMediaType.Gif:
                    dataTemplate = ImageNotificationDataTemplate;
                    break;
                case EAttachedMediaType.Video:
                    dataTemplate = VideoNotificationDataTemplate;
                    break;
                default:
                    dataTemplate = TextNotificationDataTemplate;
                    break;
            }

            return dataTemplate;
        }

        #endregion
    }
}
