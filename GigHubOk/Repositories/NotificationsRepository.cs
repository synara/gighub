using GigHubOk.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHubOk.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private ApplicationDbContext _ctx;

        public NotificationsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public List<Notification> NotificationsToUsers(string userId)
        {
            return _ctx.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }

        public List<UserNotification> NotificationsIsRead(string userId)
        {
            return _ctx.UserNotifications
                .Where(un => !un.IsRead && un.UserId == userId)
                .ToList();
        }
    }
}