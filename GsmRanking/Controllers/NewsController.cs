using AutoMapper;
using GsmRanking.Common;
using GsmRanking.Models;
using GsmRanking.Services;
using GsmRanking.Viewmodels.News;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Controllers
{
    public class NewsController : GsmRankingBaseController
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public const int ImageMaxWidth = 700;
        public const int ImageMaxHeight = 700;
        public const int ImageMinWidth = 100;
        public const int ImageMinHeight = 100;

        public NewsController([FromServices] INewsService newsService, [FromServices] IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_newsService.GetAllNews());
        }

        public IActionResult Details(int id)
        {
            var news = _newsService.GetNewsById(id);
            if(news == null)
            {
                SetError($"News with id '{id}' not found.");
                return RedirectToAction("Index");
            }

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsCreateViewModel n)
        {
            try
            {
                var news = _mapper.Map<NewsCreateViewModel, News>(n);
                
                using (var memoryStream = new MemoryStream())
                {
                    if(n.ImageUpload != null)
                    {
                        await n.ImageUpload.CopyToAsync(memoryStream);
                        using (var image = new Bitmap(Image.FromStream(memoryStream)))
                        {
                            if(!ValidateImageSize(image))
                            {
                                return View(n);
                            }
                        }
                        news.Image = Convert.ToBase64String(memoryStream.ToArray());
                        news.CreateDate = DateTime.Now;
                    }
                }
                _newsService.AddNews(news);
                SetSuccess($"Pomyślnie utworzono news '{n.Title}'");
            }
            catch (Exception ex)
            {
                SetError(ex);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        private bool ValidateImageSize(Bitmap image)
        {
            bool isValid = true;
            if (image.Width > ImageMaxWidth)
            {
                ModelState.AddModelError("ImageUpload", $"Szerokość obrazka przekracza {ImageMaxWidth}px");
                isValid = false;
            }
            if (image.Width < ImageMinWidth)
            {
                ModelState.AddModelError("ImageUpload", $"Szerokość obrazka musi być większa niż {ImageMinWidth}px");
                isValid = false;
            }
            if (image.Height > ImageMaxHeight)
            {
                ModelState.AddModelError("ImageUpload", $"Wysokość obrazka przekracza {ImageMaxHeight}px");
                isValid = false;
            }
            if (image.Height < ImageMinHeight)
            {
                ModelState.AddModelError("ImageUpload", $"Wysokość obrazka musi być większa niż {ImageMinHeight}px");
                isValid = false;
            }
            return isValid;
        }
    }
}
