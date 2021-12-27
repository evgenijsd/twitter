using InterTwitter.Helpers;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models.NotificationViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface INotificationService
    {
        Task<AOResult<List<BaseNotificationViewModel>>> GetNotificationsAsync(int userId);
    }
}
