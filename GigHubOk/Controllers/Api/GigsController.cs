using GigHubOk.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHubOk.Controllers.Api
{

    [Authorize]
    public class GigsController : ApiController
    {
        private IUnitOfWork _unitofwork;
        
        public GigsController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel (int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitofwork.Gigs.GetGigWithAttendee(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();


            _unitofwork.Complete();

            return Ok();
        }

       
    }
}
