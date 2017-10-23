using System.Collections.Generic;
using GigHubOk.Models;

namespace GigHubOk.Repositories
{
    public interface IGenresRepository
    {
        IEnumerable<Genre> GetAllGenres();
    }
}