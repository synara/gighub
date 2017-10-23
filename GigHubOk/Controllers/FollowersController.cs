using GigHubOk.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHubOk.Controllers
{
    public class FollowersController : Controller
    {
        private IUnitOfWork _unitofwork;

        public FollowersController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }


        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var followers = _unitofwork.Follows.GetFollowees(userId);

            return View(followers);
        }
        
        
        
    }
}