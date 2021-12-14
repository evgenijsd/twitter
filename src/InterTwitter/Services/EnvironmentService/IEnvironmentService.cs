using System.Drawing;

namespace InterTwitter.Services.EnvironmentService
{
    public interface IEnvironmentService
    {
        Color GetStatusBarColor();

        void SetStatusBarColor(Color color, bool darkStatusBarTint);

        bool GetUseDarkStatusBarTint();
    }
}