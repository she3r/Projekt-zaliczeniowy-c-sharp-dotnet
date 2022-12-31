using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjektZaliczeniowy.Entities;

namespace ProjektZaliczeniowy.Repositories
{
    public interface IScoresRepository
    {
        Task<IEnumerable<Score>> GetScoresAsync();
        Task<Score>  GetScoreAsync(Guid userID, Guid scoreID);
        Task CreateScoreAsync(Guid userID, Score score);
        Task UpdateScoreAsync(Guid userID, Guid scoreID, Score score);
        Task DeleteScoreAsync(Guid userID, Guid scoreID);

        Task DeleteUserAsync(Guid userID);
    }
}