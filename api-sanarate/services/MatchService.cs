using api_sanarate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _dbContext;

        public MatchService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Match>> GetAllMatchesAsync()
        {
            return await _dbContext.matches.ToListAsync();
        }

        public async Task<Match> GetMatchByIdAsync(int id)
        {
            var match = await _dbContext.matches.FindAsync(id);
            if (match == null)
            {
                throw new KeyNotFoundException($"Match con id {id} no encontrada.");
            }
            return match;
        }

        public async Task AddMatchAsync(Match match)
        {
            await _dbContext.matches.AddAsync(match);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateMatchAsync(int id, Match match)
        {
            var matchExistente = await _dbContext.matches.FindAsync(id);
            if (matchExistente == null)
            {
                return false;
            }

            matchExistente.team1_id = match.team1_id;
            matchExistente.team2_id = match.team2_id;
            matchExistente.date = match.date;
            matchExistente.venue = match.venue;
            matchExistente.score = match.score;
            matchExistente.status = match.status;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMatchAsync(int id)
        {
            var match = await _dbContext.matches.FindAsync(id);
            if (match == null)
            {
                return false;
            }

            _dbContext.matches.Remove(match);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
