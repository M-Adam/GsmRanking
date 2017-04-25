using GsmRanking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Services
{
    public class NewsService : GsmRankingBaseService, INewsService
    {
        private GsmRankingContext _context;

        public NewsService(GsmRankingContext context)
        {
            _context = context;
        }

        public void AddNews(News news)
        {
            news.CreateDate = DateTime.Now;
            _context.Add(news);
            SaveChanges();
        }

        public void DeleteNews(News news)
        {
            _context.Remove(news);
            SaveChanges();
        }

        public async Task<List<News>> GetAllNews(bool publishedOnly = false)
        {
            IQueryable<News> news;
            if(publishedOnly)
            {
                news = _context.News.Where(n => n.IsPublished);
            }
            else
            {
                news = _context.News;
            }

            return await news.ToListAsync();
        }

        public async Task<News> GetNewsById(int id)
        {
            return await _context.News.SingleOrDefaultAsync(n => n.IdNews == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public interface INewsService
    {
        Task<List<News>> GetAllNews(bool publishedOnly = false);
        Task<News> GetNewsById(int id);
        void AddNews(News news);
        void DeleteNews(News news);
        void SaveChanges();
    }
}
