using InterTwitter.Helpers;
using InterTwitter.iOS.Renderers;
using InterTwitter.iOS.Services.Permission;
using InterTwitter.iOS.Services.VideoProcessing;
using InterTwitter.Services.Permission;
using InterTwitter.Services.Video;
using InterTwitter.Services.VideoProcessing;
using Prism;
using Prism.Ioc;

namespace InterTwitter.iOS
{
    public class iOSInitializer : IPlatformInitializer
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