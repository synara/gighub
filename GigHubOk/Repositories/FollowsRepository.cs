using GigHubOk.DTOs;
using GigHubOk.Models;
using GigHubOk.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace GigHubOk.Repositories
{
    public class FollowsRepository : IFollowsRepository
    {

        private ApplicationDbContext _ctx;

        public FollowsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool FollowingOrNot(Gig gig, GigsDetailsViewModel gigsvm, string userId)
        {
           return gigsvm.Following = _ctx.Followings
                .Any(g => g.FolloweeId == gig.ArtistId && g.FollowerId == userId);
        }

        public IEnumerable<ApplicationUser> GetFollowees(string userId)
        {
            return _ctx.Followings
                    .Where(f => f.FollowerId == userId)
                    .Select(f => f.Followee)
                    .ToList();
        }
  
        public void Add(Following follow)
        {
            _ctx.Followings.Add(follow);
        }

        public void Remove(Following follow)
        {
            _ctx.Followings.Remove(follow);
        }

        public Following AllMyFollowings(FollowingDto dto, string userId)
        {
            return _ctx.Followings
                .Where(f => f.FollowerId == userId
                && f.FolloweeId == dto.FolloweeId).FirstOrDefault();
        }

        public Following NotMyFollowings(string id, string userId)
        {
            return _ctx.Followings.FirstOrDefault(a => a.FolloweeId == id && a.FollowerId == userId);
        }
    }
}