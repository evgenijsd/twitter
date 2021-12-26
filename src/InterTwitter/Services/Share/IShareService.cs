using InterTwitter.Helpers.ProcessHelpers;
using System.Threading.Tasks;

namespace InterTwitter.Services.Share
{
    public interface IShareService
    {
        Task<AOResult> ShareTextRequest(string text, string uri);
    }
}
