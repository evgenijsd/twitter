﻿using InterTwitter.Helpers;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System.Threading.Tasks;

namespace InterTwitter.Services.Video
{
    public interface IVideoService
    {
        Task<AOResult<VideoProcessingResultModel>> ProcessingVideoForPostAsync(string sourceFilePath);
    }
}
