using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GsmRanking.Models;
using GsmRanking.Services;
using Microsoft.AspNetCore.Mvc;

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
            //if (await _userService.AnyAsync(x => x.Username == username))
            //{
            //    return Json(data: $"Username {username} is already in use.");
            //}
            return Json(data: true);
        }

        [HttpGet]
        public async Task<JsonResult> VerifyEmail(string email)
        {
            //if (await _userService.AnyAsync(x => x.Email == email))
            //{
            //    return Json(data: $"Email {email} is already in use.");
            //}
            return Json(data: true);
        }
    }
}
