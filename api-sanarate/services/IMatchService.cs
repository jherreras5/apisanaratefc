using api_sanarate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Services
{
    public interface IMatchService
    {
        Task<List<Match>> GetAllMatchesAsync();
        Task<Match> GetMatchByIdAsync(int id);
        Task AddMatchAsync(Match match);
        Task<bool> UpdateMatchAsync(int id, Match match);
        Task<bool> DeleteMatchAsync(int id);
    }
}
