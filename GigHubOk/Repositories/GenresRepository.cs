using GigHubOk.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHubOk.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private ApplicationDbContext _ctx; 

        public GenresRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _ctx.Genres.ToList();
        }
    }
}