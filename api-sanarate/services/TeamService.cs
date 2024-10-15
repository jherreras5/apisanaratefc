using api_sanarate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _dbContext.teams.ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            var team = await _dbContext.teams.FindAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException($"Team con id {id} no encontrado.");
            }
            return team;
        }

        public async Task AddTeamAsync(Team team)
        {
            await _dbContext.teams.AddAsync(team);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateTeamAsync(int id, Team team)
        {
            var teamExistente = await _dbContext.teams.FindAsync(id);
            if (teamExistente == null)
            {
                return false;
            }

            teamExistente.name = team.name;
            teamExistente.logo = team.logo;
            teamExistente.description = team.description;
            teamExistente.created_at = team.created_at;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await _dbContext.teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }

            _dbContext.teams.Remove(team);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
