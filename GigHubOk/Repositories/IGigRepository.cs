using GigHubOk.Models;
using System.Collections.Generic;

namespace GigHubOk.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGigToEdit(int id, string userId);
        Gig GetGigToSeeDetails(int id);
        IEnumerable<Gig> GetGigUserAttending(string userId);
        Gig GetGigWithAttendee(int gigId);
        IEnumerable<Gig> GetMyGigs(string userId);
        IEnumerable<Gig> GetUpComingGigs();
        Gig GigsToCancel(int id, string userId);
    }
}