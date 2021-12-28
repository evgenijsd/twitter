using InterTwitter.Droid.Renderers;
using InterTwitter.Droid.Services.Permission;
using InterTwitter.Droid.Services.VideoProcessing;
using InterTwitter.Helpers;
using InterTwitter.Services.Permission;
using InterTwitter.Services.Video;
using InterTwitter.Services.VideoProcessing;
using Prism;
using Prism.Ioc;

namespace InterTwitter.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPermissionService, PermissionService>();
            containerRegistry.RegisterSingleton<IVideoProcessingService, VideoProcessingService>();
            containerRegistry.RegisterSingleton<IVideoService, VideoService>();
            containerRegistry.RegisterSingleton<IKeyboardHelper, KeyboardHelper>();
        }
    }
}
