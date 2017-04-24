using GsmRanking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<News> GetAllNews()
        {
            return _context.News.ToList();
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
        List<News> GetAllNews();
        News GetNewsById(int id);
        void AddNews(News news);
        void EditNews(News news);
        void DeleteNews(News news);
        void SaveChanges();
    }
}
