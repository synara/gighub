using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHubOk.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        protected UserNotification() { } //por causa do ef

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null) throw new ArgumentNullException("user");

            if (notification == null) throw new ArgumentException("notification");

            this.Notification = notification;
            this.User = user;
            
        }

        public void Read ()
        {
            this.IsRead = true;
        }
    }
}