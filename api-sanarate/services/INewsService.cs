using api_sanarate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Services
{
    public interface INewsService
    {
        Task<List<News>> GetAllNewsAsync();
        Task<News> GetNewsByIdAsync(int id);
        Task AddNewsAsync(News news);
        Task<bool> UpdateNewsAsync(int id, News news);
        Task<bool> DeleteNewsAsync(int id);
    }
}
