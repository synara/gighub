using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHubOk.Models
{
    public class Gig
    {
        public int Id { get; private set; }

        public bool IsCanceled { get; private set; }
  
        public ApplicationUser Artist { get; set; }
        
        [Required]
        public string ArtistId { get; set; }

        public DateTime Datetime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

       
        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }


        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }
        
        public void Cancel()
        {

            this.IsCanceled = true;
            var notification = Notification.GigCanceled(this);

            //guarda todas a notificação pra todos os usuários
            //método na ApplicationUser
            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }


        }

        internal void Update(DateTime dateTime, String venue, int genre)
        {
            var notification = Notification.GigUpdated(this, dateTime, Venue);

            Venue = venue;
            Datetime = dateTime;
            GenreId = genre;

            foreach(var attendee in this.Attendances.Select(a => a.Attendee)){
                attendee.Notify(notification);
            }

            
        }
    }
}