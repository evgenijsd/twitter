using AVFoundation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using InterTwitter.Services.VideoService;
using System;
using System.IO;
using UIKit;

namespace InterTwitter.iOS.Services.VideoService
{
    public class VideoService : IVideoService
    {
        public double TryVideoLength(string url)
        {
            double result;

            try
            {
                AVAsset avasset = AVAsset.FromUrl((new Foundation.NSUrl(url, false)));
                var length = avasset.Duration.Seconds.ToString();
                result = Convert.ToDouble(length);
            }
            catch (Exception e)
            {
                result = -1;
            }

            return result;
        }

        public Stream TryGenerateThumbImage(string url, long usecond)
        {
            Stream stream = null;

            try
            {
                AVAsset avasset = AVAsset.FromUrl((new Foundation.NSUrl(url, false)));

                AVAssetImageGenerator imageGenerator = new AVAssetImageGenerator(avasset);
                imageGenerator.AppliesPreferredTrackTransform = true;

                CMTime actualTime;
                NSError error;
                CGImage cgImage = imageGenerator.CopyCGImageAtTime(new CMTime(usecond, 1000000), out actualTime, out error);

                stream = new UIImage(cgImage).AsPNG().AsStream();
            }
            catch (Exception e)
            {
            }

            return stream;
        }
    }
}