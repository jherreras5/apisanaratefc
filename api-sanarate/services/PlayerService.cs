using api_sanarate.Models;
using Microsoft.EntityFrameworkCore;

namespace api_sanarate.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            return await _dbContext.players.ToListAsync();
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            var player = await _dbContext.players.FindAsync(id);
            if (player == null)
            {
                throw new KeyNotFoundException($"Player con id {id} no encontrado.");
            }
            return player;
        }

        public async Task AddPlayerAsync(Player player)
        {
            await _dbContext.players.AddAsync(player);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePlayerAsync(int id, Player player)
        {
            var playerExistente = await _dbContext.players.FindAsync(id);
            if (playerExistente == null)
            {
                return false;
            }

            playerExistente.team_id = player.team_id;
            playerExistente.name = player.name;
            playerExistente.position = player.position;
            playerExistente.number = player.number;
            playerExistente.photo = player.photo;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var player = await _dbContext.players.FindAsync(id);
            if (player == null)
            {
                return false;
            }

            _dbContext.players.Remove(player);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
