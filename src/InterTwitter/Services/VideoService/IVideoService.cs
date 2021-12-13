using Xamarin.Forms;

namespace InterTwitter.Services.VideoService
{
    public interface IVideoService
    {
        double VideoLength(string url);

        ImageSource GenerateThumbImage(string url, long usecond);
    }
}