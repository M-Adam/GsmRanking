using GsmRanking.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Controllers
{
    public class NewsController : GsmRankingBaseController
    {
        private INewsService _newsService;

        public NewsController([FromServices] INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Details(int newsId)
        {
            return View(_newsService.GetAllNews());
        }
    }
}
