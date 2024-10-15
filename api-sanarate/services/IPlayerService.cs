using api_sanarate.Models;

namespace api_sanarate.Services
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task AddPlayerAsync(Player player);
        Task<bool> UpdatePlayerAsync(int id, Player player);
        Task<bool> DeletePlayerAsync(int id);
    }
}
