using InterTwitter.Helpers.ProcessHelpers;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.Services.Share
{
    public class ShareService : IShareService
    {
        public async Task<AOResult> ShareTextRequest(string title, string uri)
        {
            var aOResult = new AOResult();

            try
            {
                await Xamarin.Essentials.Share.RequestAsync(new ShareTextRequest
                {
                    Text = title,
                    Uri = uri,
                });

                aOResult.SetSuccess();
            }
            catch (Exception ex)
            {
                aOResult.SetError($"{nameof(ShareTextRequest)} : exception", "Something went wrong", ex);
            }

            return aOResult;
        }
    }
}
