using GigHubOk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHubOk.Repositories
{
    public class GigRepository : IGigRepository
    {
        private ApplicationDbContext _ctx;
         
        public GigRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }


        public Gig GetGigWithAttendee(int gigId)
        {
            return _ctx.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(a => a.Id == gigId);
        }
        public IEnumerable<Gig> GetGigUserAttending(string userId)
        {
            return _ctx.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(g => g.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetMyGigs(string userId)
        {
           return _ctx.Gigs
                .Where(g => g.ArtistId == userId && g.Datetime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigToEdit(int id, string userId)
        {
            return _ctx.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
        }

        public Gig GetGigToSeeDetails(int id)
        {
            return _ctx.Gigs
                            .Include(g => g.Artist)
                            .Include(g => g.Genre)
                            .SingleOrDefault(g => g.Id == id);
        }

        public void Add(Gig gig)
        {
            _ctx.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetUpComingGigs()
        {
            return _ctx.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.Datetime > DateTime.Now && !g.IsCanceled);
        }

        public Gig GigsToCancel(int id, string userId)
        {
     
            return _ctx.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);
        }
    }
}