using GigHubOk.Repositories;

namespace GigHubOk.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attedances { get; }
        IFollowsRepository Follows { get; }
        IGenresRepository Genres { get; }
        IGigRepository Gigs { get; }
        INotificationsRepository Notifications { get; }

        void Complete();
    }
}