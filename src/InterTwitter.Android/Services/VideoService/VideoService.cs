using Android.Graphics;
using Android.Media;
using InterTwitter.Services.VideoService;
using System;
using System.IO;
using Xamarin.Forms;

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

        public ImageSource GenerateThumbImage(string url, long usecond)
        {
            MediaMetadataRetriever retriever = new MediaMetadataRetriever();
            retriever.SetDataSource(url);

            Bitmap bitmap = retriever.GetFrameAtTime(usecond);
            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bitmapData = stream.ToArray();
                return ImageSource.FromStream(() => new MemoryStream(bitmapData));
            }
            return null;
        }
    }
}