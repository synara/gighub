using GigHubOk.DTOs;
using GigHubOk.Models;
using GigHubOk.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHubOk.Controllers.Api
{
    //usuários para gigs

    [Authorize]
    public class AttendancesController : ApiController
    {
        private IUnitOfWork _unitofwork;
        private ApplicationDbContext ctx;

        public AttendancesController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;

        }

        [HttpPost]
        public IHttpActionResult Atend(AttendanceDtos dto)
        {
            var attendeeId = User.Identity.GetUserId();
            Attendance att = _unitofwork.Attedances.GetAttendance(dto.GigId, attendeeId);

            if (att != null)
                return BadRequest("Attendance is already done");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = attendeeId
            };

            _unitofwork.Attedances.Add(attendance);
            _unitofwork.Complete();

            return Ok();
        }



        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitofwork.Attedances.GetAttendance(id, userId);

            if (attendance == null) return NotFound();

            if (attendance.AttendeeId != userId) return Unauthorized();

            _unitofwork.Attedances.Remove(attendance);
            _unitofwork.Complete();

            return Ok();
        }

    }

}

