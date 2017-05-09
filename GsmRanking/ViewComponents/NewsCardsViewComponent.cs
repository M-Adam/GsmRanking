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
        private readonly INewsService _newsService;
        
        public NewsCardsViewComponent([FromServices] INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<News> allNews = await _newsService.GetAllNews(true);
            var model = new List<NewsCardViewModel>();

            foreach (var news in allNews)
            {
                model.Add(new NewsCardViewModel()
                {
                    Url = Url.Action("Details", "News", new { id = news.IdNews }),
                    Title = news.Title,
                    ShortDescription = news.ShortText,
                    IsPublished = news.IsPublished,
                    PublishDate = news.PublishDate,
                    Image = news.Image,
                    CommentCount = news.Comments.Count,
                    ViewsCount = news.ViewsCount,
                    AuthorName = news.IdAutorNavigation.Username
                });
            }

            return View("~/Views/Shared/Components/_NewsCards.cshtml", model);
        }
    }
}

