using AVFoundation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using InterTwitter.Services.VideoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

namespace InterTwitter.iOS.Services.VideoService
{
    public class VideoService : IVideoService
    {
        public double VideoLength(string url)
        {
            AVAsset avasset = AVAsset.FromUrl((new Foundation.NSUrl(url, false)));
            var length = avasset.Duration.Seconds.ToString();
 
            return Convert.ToDouble(length);
        }

        public ImageSource GenerateThumbImage(string url, long usecond)
        {
            AVAsset avasset = AVAsset.FromUrl((new Foundation.NSUrl(url, false)));

            AVAssetImageGenerator imageGenerator = new AVAssetImageGenerator(avasset);
            imageGenerator.AppliesPreferredTrackTransform = true;

            CMTime actualTime;
            NSError error;
            CGImage cgImage = imageGenerator.CopyCGImageAtTime(new CMTime(usecond, 1000000), out actualTime, out error);

            return ImageSource.FromStream(() => new UIImage(cgImage).AsPNG().AsStream());
        }
    }
}