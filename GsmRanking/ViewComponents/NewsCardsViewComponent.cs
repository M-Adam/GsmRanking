using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GsmRanking.Viewmodels.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GsmRanking.ViewComponents
{
    public class NewsCardsViewComponent : ViewComponent
    {
        private readonly DbContext _context;

        public NewsCardsViewComponent(DbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        { 
            //orderBy Date.skip 5.take 4

            var model = new NewsCardViewModel()
            {

            };
            return View("~/Views/Shared/Components/_NewsCards.cshtml", model);
        }
    }
}

