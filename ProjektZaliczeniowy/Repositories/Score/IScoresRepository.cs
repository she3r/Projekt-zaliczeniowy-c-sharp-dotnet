using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjektZaliczeniowy.Entities;

namespace ProjektZaliczeniowy.Repositories
{
    public interface IScoresRepository
    {
        Task<IEnumerable<Score>> GetAllScoresOfStudentAsync(string studentName);
        Task<Score> GetScoreOfStudentAsync(string studentName, Guid scoreId);    // guid is global (unique among all students)
        Task CreateScoreAsync(Score score);     // in score saved studentName
        Task UpdateScoreOfStudentAsync(Score score);
        Task DeleteScoreAsync(string studentName, Guid id);
        Task DeleteAllScoresAsync(string studentName);
    }
}