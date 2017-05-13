using GsmRanking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace GsmRanking.Services
{
    public class NewsService : GsmRankingBaseService, INewsService
    {
        private GsmRankingContext _context;

        private IIncludableQueryable<News, User> NewsInclude => _context
            .News
            .Include(x => x.IdAutorNavigation)
            .Include(x => x.Comments)
            .ThenInclude(x => x.IdAutorNavigation);

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
            var news = NewsInclude.AsNoTracking();
            if(publishedOnly)
            {
                news = news.Where(n => n.IsPublished);
            }
            
            return await news.OrderByDescending(x=>x.PublishDate).ToListAsync();
        }

        public async Task<News> GetNewsById(int id)
        {
            return await NewsInclude.FirstOrDefaultAsync(n => n.IdNews == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddComment(string commentContent, int newsId, int userId)
        {
            _context.Comments.Add(new Comment()
            {
                Content = commentContent,
                CreateDate = DateTime.Now,
                IdAutor = userId,
                IdNews = newsId
            });
            SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                SaveChanges();
            }
        }
    }

    public interface INewsService
    {
        Task<List<News>> GetAllNews(bool publishedOnly = false);
        Task<News> GetNewsById(int id);
        void AddNews(News news);
        void DeleteNews(News news);
        void SaveChanges();
        void AddComment(string commentContent, int newsId, int userId);
        void DeleteComment(int commentId);
    }
}
