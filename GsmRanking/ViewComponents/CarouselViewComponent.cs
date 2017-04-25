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
    public class CarouselViewComponent : ViewComponent
    {
        private INewsService _newsService;

        public CarouselViewComponent([FromServices] INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<News> allNews = await _newsService.GetAllNews(true);
            List<NewsSlideViewModel> model = new List<NewsSlideViewModel>();
            foreach (var news in allNews)
            {
                model.Add(new NewsSlideViewModel()
                {
                    Url = Url.Action("Details", "News", new { id = news.IdNews }),
                    Title = news.Title,
                    ShortDescription = news.ShortText,
                    Image = news.Image
                });
            }
            return View("~/Views/Shared/Components/_Carousel.cshtml", model);
        }
    }
}
