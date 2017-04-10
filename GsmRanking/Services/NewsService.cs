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

        public IEnumerable<News> GetAllNews()
        {
            return _context.News;
        }
    }

    public interface INewsService
    {
        IEnumerable<News> GetAllNews();
    }
}
