using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GsmRanking.Models;
using GsmRanking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GsmRanking.Controllers
{
    public class ValidationController : Controller
    {
        private readonly GsmRankingContext _context;

        public ValidationController(GsmRankingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> VerifyUsername(string username)
        {
            if(await _context.Users.AnyAsync(x=>x.Username == username))
            {
                return Json(data: $"Nazwa użytkownika {username} jest już zajęta.");
            }
            return Json(data: true);
        }

        [HttpGet]
        public async Task<JsonResult> VerifyEmail(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
            {
                return Json(data: $"Email {email} jest już zajęty.");
            }
            return Json(data: true);
        }
    }
}
