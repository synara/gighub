using GigHubOk.DTOs;
using GigHubOk.Models;
using GigHubOk.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHubOk.Controllers.Api
{
    public class FollowingsController : ApiController
    {

        //usuários para usuários

        IUnitOfWork _unitofwork;
        ApplicationDbContext ctx;

        public FollowingsController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            Following follow = _unitofwork.Follows.AllMyFollowings(dto, userId);

            if (follow != null)
            {
                return BadRequest("Following already exists");
            }

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _unitofwork.Follows.Add(following);
            _unitofwork.Complete();


            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();
            Following unf = _unitofwork.Follows.NotMyFollowings(id, userId);

            if (unf == null) return NotFound();

            _unitofwork.Follows.Remove(unf);
            _unitofwork.Complete();

            return Ok();
        }

        
    }
}
