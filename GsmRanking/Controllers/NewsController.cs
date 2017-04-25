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
using Microsoft.AspNetCore.Http;

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

        public async Task<IActionResult> Index()
        {
            var news = await _newsService.GetAllNews();
            return View(news);
        }

        public IActionResult Details(int id)
        {
            var news = _newsService.GetNewsById(id);
            if(news == null)
            {
                SetError($"News with id '{id}' not found.");
                return RedirectToAction("Index");
            }
            news.ViewsCount++;
            _newsService.EditNews(news);
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsCreateViewModel model)
        {
            var news = _mapper.Map<NewsCreateViewModel, News>(model);

            if (model.ImageUpload != null)
            {
                string imageString = await GetImageBase64FromFile(model.ImageUpload);
                if (!String.IsNullOrEmpty(imageString))
                {
                    news.Image = imageString;
                }
                else
                {
                    return View(model);
                }
            }

            _newsService.AddNews(news);
            SetSuccess($"Pomyślnie utworzono news '{model.Title}'");

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                SetError("Id newsa do edycji nie może być puste");
                return RedirectToAction("Index");
            }
            var news = _newsService.GetNewsById(id.Value);
            if(news == null)
            {
                SetError($"Nie znaleziono newsa o id: {id}");
                return RedirectToAction("Index");
            }
            var newsViewModel = _mapper.Map<News, NewsEditViewModel>(news);
            return View(newsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsEditViewModel model)
        {
            try
            {
                var existingNews = _newsService.GetNewsById(model.IdNews);
                Mapper.Map(model, existingNews);

                if (model.ImageUpload != null)
                {
                    string imageString = await GetImageBase64FromFile(model.ImageUpload);
                    if (!String.IsNullOrEmpty(imageString))
                    {
                        existingNews.Image = imageString;
                    }
                    else
                    {
                        return View(model);
                    }
                }
                _newsService.EditNews(existingNews);
                SetSuccess($"Pomyślnie edytowano news '{model.Title}'");
            }
            catch (Exception ex)
            {
                SetError(ex);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var news = _newsService.GetNewsById(id);
            if (news == null)
            {
                SetError($"Nie znaleziono newsa o id: {id}");
                return RedirectToAction("Index");
            }
            _newsService.DeleteNews(news);
            SetSuccess("News został usunięty");

            return RedirectToAction("Index");
        }

        public IActionResult Publish(int? id, bool publish)
        {
            if(!id.HasValue)
            {
                SetError("Id newsa do opublikowania nie może być puste");
                return View("Index");
            }
            var news = _newsService.GetNewsById(id.Value);
            if (news == null)
            {
                SetError($"Nie znaleziono newsa o id: {id}");
                return RedirectToAction("Index");
            }
            if (news.IsPublished == publish)
            {
                return View("Index");
            }

            news.IsPublished = publish;
            if(publish)
            {
                news.PublishDate = DateTime.Now;
                SetSuccess($"Opublikowano newsa '{news.Title}'");
            }
            else
            {
                SetSuccess($"Ukryto newsa '{news.Title}'");
            }
            _newsService.EditNews(news);
            return RedirectToAction("Index");
        }

        private async Task<string> GetImageBase64FromFile(IFormFile imageUpload)
        {
            string output = string.Empty;
            if (imageUpload != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageUpload.CopyToAsync(memoryStream);
                    using (var image = new Bitmap(Image.FromStream(memoryStream)))
                    {
                        bool isValid = ValidateUploadedImageSize(image.Width, image.Height, "ImageUpload");
                        if (isValid)
                        {
                            output = Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }
            return output;
        }

        private bool ValidateUploadedImageSize(int width, int height, string validationErrorKey)
        {
            bool isValid = true;
            if (width > ImageMaxWidth)
            {
                ModelState.AddModelError(validationErrorKey, $"Szerokość obrazka przekracza {ImageMaxWidth}px");
                isValid = false;
            }
            if (width < ImageMinWidth)
            {
                ModelState.AddModelError(validationErrorKey, $"Szerokość obrazka musi być większa niż {ImageMinWidth}px");
                isValid = false;
            }
            if (height > ImageMaxHeight)
            {
                ModelState.AddModelError(validationErrorKey, $"Wysokość obrazka przekracza {ImageMaxHeight}px");
                isValid = false;
            }
            if (height < ImageMinHeight)
            {
                ModelState.AddModelError(validationErrorKey, $"Wysokość obrazka musi być większa niż {ImageMinHeight}px");
                isValid = false;
            }
            return isValid;
        }
    }
}
