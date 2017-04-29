using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GsmRanking.Viewmodels.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using GsmRanking.Services;
using GsmRanking.Models;

namespace GsmRanking.ViewComponents
{
    public class NewsCardsViewComponent : ViewComponent
    {
        private INewsService _newsService;
        
        public NewsCardsViewComponent([FromServices] INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<News> allNews = await _newsService.GetAllNews(true);
            List<NewsCardViewModel> model = new List<NewsCardViewModel>();

            foreach (var news in allNews)
            {
                model.Add(new NewsCardViewModel()
                {
                    Url = Url.Action("Details", "News", new { id = news.IdNews }),
                    Title = news.Title,
                    ShortDescription = news.ShortText,
                    IsPublished = news.IsPublished,
                    PublishDate = news.PublishDate,
                    Image = news.Image
                });
            }

            return View("~/Views/Shared/Components/_NewsCards.cshtml", model);
        }
    }
}

