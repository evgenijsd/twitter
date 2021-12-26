using InterTwitter.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XShare = Xamarin.Essentials.Share;

namespace InterTwitter.Services.Share
{
    public class ShareService : IShareService
    {
        public async Task<AOResult> ShareRequestAsync(string text)
        {
            var aOResult = new AOResult();

            try
            {
                await XShare.RequestAsync("share text");
                aOResult.SetSuccess();
            }
            catch (Exception ex)
            {
                aOResult.SetError($"{nameof(ShareRequestAsync)} : exception", "Something went wrong", ex);
            }

            return aOResult;
        }

        public async Task<AOResult> ShareTextRequest(string text, string title)
        {
            var aOResult = new AOResult();

            try
            {
                await XShare.RequestAsync(new ShareTextRequest(text, title)
                {
                    Subject = "User profile in InterTwitter",
                    Uri = "https://get.wallhere.com/photo/1920x1200-px-animals-tiger-1098861.jpg",
                    PresentationSourceBounds = new System.Drawing.Rectangle() { Height = 50, Width = 50, X = 10, Y = 30 },
                    Text = text,
                    Title = title,
                });

                aOResult.SetSuccess();
            }
            catch (Exception ex)
            {
                aOResult.SetError($"{nameof(ShareRequestAsync)} : exception", "Something went wrong", ex);
            }

            return aOResult;
        }

        public async Task<AOResult> ShareFileAsync(string title, ShareFile file)
        {
            var aOResult = new AOResult();

            try
            {
                await XShare.RequestAsync(new ShareFileRequest
                {
                    Title = title,
                    File = file,
                });

                aOResult.SetSuccess();
            }
            catch (Exception ex)
            {
                aOResult.SetError($"{nameof(ShareRequestAsync)} : exception", "Something went wrong", ex);
            }

            return aOResult;
        }
    }
}
