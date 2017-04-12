using AutoMapper;
using GsmRanking.Common;
using GsmRanking.Models;
using GsmRanking.Services;
using GsmRanking.Viewmodels.News;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Controllers
{
    public class NewsController : GsmRankingBaseController
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

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
                TempData[ViewContextExtension.ErrorKey] = $"News with id '{id}' not found.";
                return RedirectToAction("Index");
            }

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsCreateViewModel n)
        {
            //TODO: Add model validation, implement UI messages
            try
            {
                var news = _mapper.Map<NewsCreateViewModel, News>(n);
                
                //TODO: Image size constraints validation
                using (var memoryStream = new MemoryStream())
                {
                    if(n.ImageUpload != null)
                    {
                        await n.ImageUpload.CopyToAsync(memoryStream);
                        news.Image = Convert.ToBase64String(memoryStream.ToArray());
                        news.Createdate = DateTime.Now;
                    }
                    
                }
                _newsService.AddNews(news);
                TempData[ViewContextExtension.SuccessKey] = $"News: '{n.Title}' has been created.";
            }
            catch (Exception ex)
            {
                TempData[ViewContextExtension.ErrorKey] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
