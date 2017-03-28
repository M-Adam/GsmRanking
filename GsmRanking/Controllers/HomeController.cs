using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GsmRanking.Viewmodels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GsmRanking.Controllers
{
    public class HomeController : GsmRankingBaseController
    {
        public IActionResult Index()
        {
            var viewModel = new HomepageViewModel()
            {
                
            };
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
