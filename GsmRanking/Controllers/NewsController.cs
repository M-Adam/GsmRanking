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
    using System.Security.Claims;
    using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GsmRanking.Common.Authorization;

namespace GsmRanking.Controllers
{
    public class NewsController : GsmRankingBaseController
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public const int ImageMaxWidth = 1000;
        public const int ImageMaxHeight = 1000;
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

        public async Task<IActionResult> Details(int id)
        {
            var news = await _newsService.GetNewsById(id);
            if (news == null)
            {
                SetError($"News with id '{id}' not found.");
                return RedirectToAction("Index");
            }
            news.ViewsCount++;
            _newsService.SaveChanges();
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PreventDuplicateRequest]
        [Authorize(Policy = Policies.Editor)]
        public async Task<IActionResult> Create(NewsCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            try
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
            }
            catch (Exception ex)
            {
                SetError(ex);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Policy = Policies.Editor)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = Policies.Editor)]
        public async Task<IActionResult> Edit(int id)
        {
            var news = await _newsService.GetNewsById(id);
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
        [PreventDuplicateRequest]
        [Authorize(Policy = Policies.Editor)]
        public async Task<IActionResult> Edit(NewsEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var existingNews = await _newsService.GetNewsById(model.IdNews);
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
                _newsService.SaveChanges();
                SetSuccess($"Pomyślnie edytowano news '{model.Title}'");
            }
            catch (Exception ex)
            {
                SetError(ex);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Policy = Policies.Editor)]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsService.GetNewsById(id);
            if (news == null)
            {
                SetError($"Nie znaleziono newsa o id: {id}");
                return RedirectToAction("Index");
            }
            _newsService.DeleteNews(news);
            SetSuccess("News został usunięty");

            return RedirectToAction("Index");
        }

        [Authorize(Policy = Policies.Editor)]
        public async Task<IActionResult> Publish(int id, bool publish)
        {
            var news = await _newsService.GetNewsById(id);
            if (news == null)
            {
                SetError($"Nie znaleziono newsa o id: {id}");
                return RedirectToAction("Index");
            }
            if (news.IsPublished != publish)
            {
                news.IsPublished = publish;
                if (publish)
                {
                    news.PublishDate = DateTime.Now;
                    SetSuccess($"Opublikowano newsa '{news.Title}'");
                }
                else
                {
                    SetSuccess($"Ukryto newsa '{news.Title}'");
                }
                _newsService.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [NonAction]
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

        [NonAction]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AddComment(string commentContent, int newsId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(commentContent) || commentContent.Length > 150)
                {
                    SetError("Niedozwolona długość komentarza.");
                }
                else
                {
                    _newsService.AddComment(commentContent, newsId,
                        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                    SetSuccess("Pomyślnie dodano komentarz!");
                }
            }
            catch (Exception e)
            {
                SetError(e);
            }
            
            return RedirectToAction("Details", new { id = newsId });
        }

        [HttpPost]
        [Authorize(Policy = Policies.Editor)]
        public IActionResult DeleteComment(int commentId)
        {
            try
            {
                _newsService.DeleteComment(commentId);
                SetSuccess("Pomyślnie usunięto komentarz");
            }
            catch (Exception e)
            {
                SetError(e);
            }
            
            return Ok();
        }
    }
}
