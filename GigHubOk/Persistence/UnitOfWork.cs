using GigHubOk.Models;
using GigHubOk.Repositories;

namespace GigHubOk.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;

        public IGigRepository Gigs { get; private set; }
        public IGenresRepository Genres { get; private set; }
        public IFollowsRepository Follows { get; private set; }
        public IAttendanceRepository Attedances { get; private set; }
        public INotificationsRepository Notifications { get; set; }

        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            Gigs = new GigRepository(ctx);
            Genres = new GenresRepository(ctx);
            Follows = new FollowsRepository(ctx);
            Attedances = new AttendanceRepository(ctx);
            Notifications = new NotificationsRepository(ctx);
        }


        public void Complete()
        {
            _ctx.SaveChanges();
        }
    }
}