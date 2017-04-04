using System.Collections.Generic;
using System.Threading.Tasks;
using GsmRanking.Viewmodels.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GsmRanking.ViewComponents
{
    public class CarouselViewComponent : ViewComponent
    {
        private readonly DbContext _context;
        
        public CarouselViewComponent(DbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //OrderBy Date.Take 5
            var model = new NewsCardViewModel()
            {
                
            };
            return View("~/Views/Shared/Components/_Carousel.cshtml", model);
        }

        
    }
}
