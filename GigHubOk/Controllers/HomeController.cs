using GigHubOk.Persistence;
using GigHubOk.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;


namespace GigHubOk.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitofwork;

        public HomeController(UnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public ActionResult Index(string query = null)
        {

            var upComing = _unitofwork.Gigs.GetUpComingGigs();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upComing = upComing
                    .Where(g =>
                    g.Artist.Name.Contains(query) || 
                    g.Genre.Name.Contains(query) || 
                    g.Venue.Contains(query));
            }

            var userId = User.Identity.GetUserId();

            var attendances = _unitofwork.Attedances.GetFutureAttendances(userId)
                .ToList().ToLookup(a => a.GigId);


            var viewModel = new HomeViewModel
            {
                UpcommingGigs = upComing,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}