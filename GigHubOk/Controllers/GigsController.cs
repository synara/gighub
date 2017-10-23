using GigHubOk.Models;
using GigHubOk.Persistence;
using GigHubOk.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHubOk.Controllers
{
    public class GigsController : Controller
    {
        
        private IUnitOfWork unitofwork;

        public GigsController(IUnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }


        [Authorize]
        public ActionResult Mine()
        {
            var mygigs = unitofwork.Gigs.GetMyGigs(User.Identity.GetUserId());

            return View(mygigs);
        }

        [Authorize]
        public ActionResult Attending()
        {

            var userId = User.Identity.GetUserId();

            //var gigs = gigrepository.GetGigUserAttending(userId);

            //var attendances = attendancerepository.GetFutureAttendances(userId);

            var viewModel = new HomeViewModel
            {
                UpcommingGigs = unitofwork.Gigs.GetGigUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = unitofwork.Attedances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);

        }



        [HttpPost]
        public ActionResult Search(HomeViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }



        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Heading = "Add a Gig",
                Genres = unitofwork.Genres.GetAllGenres()
            };

            return View("GigForm", viewModel);
        }

       

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = unitofwork.Gigs.GetGigToEdit(id, User.Identity.GetUserId());
            var genres = unitofwork.Genres.GetAllGenres();

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Heading = "Edit a Gig",
                Genres = genres,
                Date = gig.Datetime.ToString("dd/MM/yyyy"),
                Time = gig.Datetime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue

            };

            return View("GigForm", viewModel);
        }

       

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel view)
        {

            if (!ModelState.IsValid)
            {
                view.Genres = unitofwork.Genres.GetAllGenres();

                return View("Create", view);

            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                Datetime = view.GetDateTime(),
                GenreId = view.Genre,
                Venue = view.Venue
            };

            unitofwork.Gigs.Add(gig);
            unitofwork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel view)
        {

            if (!ModelState.IsValid)
            {
                view.Genres = unitofwork.Genres.GetAllGenres();
                return View("GigForm", view);
            }


            var gig = unitofwork.Gigs.GetGigWithAttendee(view.Id);

            if (gig == null) return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId()) return new HttpUnauthorizedResult();

            gig.Update(view.GetDateTime(), view.Venue, view.Genre);


            unitofwork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }


        public ActionResult Details(int id)
        {

            var gig = unitofwork.Gigs.GetGigToSeeDetails(id);

            if (gig == null) return HttpNotFound();

            var gigsvm = new GigsDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                gigsvm.Attending = unitofwork.Attedances.GetUserAttendance(gig, userId);
                gigsvm.Following = unitofwork.Follows.FollowingOrNot(gig, gigsvm, userId);
            }


            return View("Details", gigsvm);

        }


    }
}