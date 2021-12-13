using Android.Graphics;
using Android.Media;
using InterTwitter.Services.VideoService;
using System;
using System.IO;

namespace InterTwitter.Droid.Services.VideoService
{
    public class VideoService : IVideoService
    {
        public double VideoLength(string url)
        {
            MediaMetadataRetriever retriever = new MediaMetadataRetriever();
            retriever.SetDataSource(url);

            var length = retriever.ExtractMetadata(MetadataKey.Duration);

            return Convert.ToDouble(length) / 1000;
        }

        public System.IO.Stream GenerateThumbImage(string url, long usecond)
        {
            MediaMetadataRetriever retriever = new MediaMetadataRetriever();
            retriever.SetDataSource(url);

            Bitmap bitmap = retriever.GetFrameAtTime(usecond);

            MemoryStream stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);

            return stream;
        }
    }
}