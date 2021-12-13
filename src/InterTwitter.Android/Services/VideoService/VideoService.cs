using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InterTwitter.Services.VideoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterTwitter.Droid.Services.VideoService
{
    public class VideoService : IVideoService
    {
        public int VideoLength(string url)
        {
            MediaMetadataRetriever retriever = new MediaMetadataRetriever();
            retriever.SetDataSource(url);
            var length = retriever.ExtractMetadata(MetadataKey.Duration);

            return Convert.ToInt32(length) / 1000;
        }
    }
}