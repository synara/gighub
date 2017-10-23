using GigHubOk.DTOs;
using GigHubOk.Models;
using GigHubOk.ViewModels;
using System.Collections.Generic;

namespace GigHubOk.Repositories
{
    public interface IFollowsRepository
    {
        bool FollowingOrNot(Gig gig, GigsDetailsViewModel gigsvm, string userId);
        IEnumerable<ApplicationUser> GetFollowees(string userId);
        Following AllMyFollowings(FollowingDto dto, string userId);
        void Add(Following follow);
        void Remove(Following follow);
        Following NotMyFollowings(string id, string userId);
    }

}