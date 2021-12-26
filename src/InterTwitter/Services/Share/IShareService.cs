using InterTwitter.Helpers;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.Services.Share
{
    public interface IShareService
    {
        Task<AOResult> ShareRequestAsync(string text);

        Task<AOResult> ShareTextRequest(string text, string title);

        Task<AOResult> ShareFileAsync(string title, ShareFile file);
    }
}
