using System.IO;

namespace InterTwitter.Services.VideoService
{
    public interface IVideoService
    {
        double VideoLength(string url);

        Stream GenerateThumbImage(string url, long usecond);
    }
}