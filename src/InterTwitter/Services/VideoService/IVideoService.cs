using System.IO;

namespace InterTwitter.Services.VideoService
{
    public interface IVideoService
    {
        double TryVideoLength(string url);

        Stream TryGenerateThumbImage(string url, long usecond);
    }
}