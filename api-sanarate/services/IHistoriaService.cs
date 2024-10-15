using api_sanarate.Models;

namespace api_sanarate.Services
{
    public interface IHistoriaService
    {
        // Define los métodos que quieres exponer en la interfaz, por ejemplo:
        Task<List<Historia>> GetAllHistoriasAsync();
        Task<Historia> GetHistoriaByIdAsync(int id);
        Task AddHistoriaAsync(Historia historia);
        Task<bool> UpdateHistoriaAsync(int id, Historia historia);
        Task<bool> DeleteHistoriaAsync(int id);
    }
}
