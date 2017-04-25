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
            _context.SaveChanges();
        }

        public void DeleteNews(News news)
        {
            _context.Remove(news);
            _context.SaveChanges();
        }

        public void EditNews(News updatedNews)
        {
            _context.SaveChanges();
        }

        public async Task<List<News>> GetAllNews(bool publishedOnly = false)
        {
            if(publishedOnly)
            {
                return await _context.News.Where(n => n.IsPublished).ToListAsync();
            }
            return await _context.News.ToListAsync();
        }

        public News GetNewsById(int id)
        {
            return _context.News.SingleOrDefault(n => n.IdNews == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public interface INewsService
    {
        Task<List<News>> GetAllNews(bool publishedOnly = false);
        News GetNewsById(int id);
        void AddNews(News news);
        void EditNews(News news);
        void DeleteNews(News news);
        void SaveChanges();
    }
}
