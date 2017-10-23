using GigHubOk.Models;
using System.Collections.Generic;

namespace GigHubOk.Repositories
{
    public interface INotificationsRepository
    {
        List<Notification> NotificationsToUsers(string userId);
        List<UserNotification> NotificationsIsRead(string userId);
    }
}