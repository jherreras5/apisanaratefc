using api_sanarate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _dbContext;

        public NewsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<News>> GetAllNewsAsync()
        {
            return await _dbContext.news.ToListAsync();
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _dbContext.news.FindAsync(id);
        }

        public async Task AddNewsAsync(News news)
        {
            await _dbContext.news.AddAsync(news);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateNewsAsync(int id, News news)
        {
            var existingNews = await _dbContext.news.FindAsync(id);
            if (existingNews == null)
            {
                return false;
            }

            existingNews.title = news.title;
            existingNews.content = news.content;
            existingNews.image = news.image;
            existingNews.published_at = news.published_at;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            var news = await _dbContext.news.FindAsync(id);
            if (news == null)
            {
                return false;
            }

            _dbContext.news.Remove(news);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
