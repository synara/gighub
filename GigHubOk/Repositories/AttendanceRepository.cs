using GigHubOk.DTOs;
using GigHubOk.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHubOk.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _ctx;

        public AttendanceRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _ctx.Attendances
                          .Where(a => a.AttendeeId == userId && a.Gig.Datetime > DateTime.Now)
                          .ToList();
        }

        public bool GetUserAttendance(Gig gig, string userId)
        {
            return _ctx.Attendances
                                .Any(g => g.GigId == gig.Id && g.AttendeeId == userId);

        }

        public Attendance MyAttendances(AttendanceDtos dto, string attendeeId)
        {
            return _ctx.Attendances
                .Where(g => g.GigId == dto.GigId && g.AttendeeId == attendeeId)
                .FirstOrDefault();


        }
        public void Add(Attendance attendance)
        {
            _ctx.Attendances.Add(attendance);
        }
        

        public void Remove(Attendance attendance)
        {
            _ctx.Attendances.Remove(attendance);
        }

        public Attendance GetAttendance(int id, string userId)
        {
            return _ctx.Attendances
                            .FirstOrDefault(a => a.AttendeeId == userId && a.GigId == id);
        }
    }

}