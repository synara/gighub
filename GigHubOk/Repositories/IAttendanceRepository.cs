using GigHubOk.DTOs;
using GigHubOk.Models;
using System.Collections.Generic;

namespace GigHubOk.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        bool GetUserAttendance(Gig gig, string userId);
        Attendance MyAttendances(AttendanceDtos dto, string attendeeId);
        void Add(Attendance attendance);
        Attendance GetAttendance(int id, string userId);
        void Remove(Attendance attendance);
    }
}