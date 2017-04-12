using GsmRanking.Models;
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
            _context.Add(news);
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
    }

    public interface INewsService
    {
        List<News> GetAllNews();
        News GetNewsById(int id);
        void AddNews(News news);
    }
}
