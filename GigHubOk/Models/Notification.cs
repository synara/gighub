using System;
using System.ComponentModel.DataAnnotations;

namespace GigHubOk.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification() { }

        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null) throw new ArgumentNullException("gig");

            this.Gig = gig;
            this.Type = type;
            this.DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            var notification = new Notification(gig, NotificationType.GigCreated);

            return notification;
        }

        public static Notification GigUpdated(Gig gig, DateTime OriginalDateTime, string OriginalVenue)
        {
            var notification = new Notification(gig, NotificationType.GigUpdate);
            notification.OriginalDateTime = OriginalDateTime;
            notification.OriginalVenue = OriginalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            var notification = new Notification(gig, NotificationType.GigCanceled);
            return notification;
        }
    }

}