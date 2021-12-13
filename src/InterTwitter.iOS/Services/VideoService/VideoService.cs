using AVFoundation;
using Foundation;
using InterTwitter.Services.VideoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace InterTwitter.iOS.Services.VideoService
{
    public class VideoService : IVideoService
    {
        public int VideoLength(string url)
        {
            AVAsset avasset = AVAsset.FromUrl((new Foundation.NSUrl(url)));
            var length = avasset.Duration.Seconds.ToString();
 
            return Convert.ToInt32(length) / 1000;
        }
    }
}