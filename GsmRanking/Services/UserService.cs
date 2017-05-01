using GsmRanking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Services
{
    public class UserService : GsmRankingBaseService, IUserService
    {
        private GsmRankingContext _context;

        public UserService(GsmRankingContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
            SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Remove(user);
            SaveChanges();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(n => n.IdUser == id);
        }

        public async Task<bool> CheckIfLoginExists(string login)
        {
            return await _context.Users.AnyAsync(u => u.Username == login);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<bool> CheckIfLoginExists(string login);
        void AddUser(User user);
        void DeleteUser(User user);
        void SaveChanges();
    }
}
