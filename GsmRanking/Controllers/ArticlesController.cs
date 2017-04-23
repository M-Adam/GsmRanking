using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GsmRanking.Common.Authorization;
using GsmRanking.Common.Enums;
using GsmRanking.Models;
using GsmRanking.Viewmodels.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GsmRanking.Controllers
{
    public class ArticlesController : GsmRankingBaseController
    {
        private readonly GsmRankingContext _context;
        private readonly IMapper _mapper;

        public ArticlesController(GsmRankingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Details(int id)
        {
            var article = _context.Articles.Find(id);
            if(article == null)
            {
                SetError("Nie znaleziono artykułu");
                return RedirectToAction("Index", "Home");
            }
            var vm = _mapper.Map<ArticleDetailsViewModel>(article);
            return View(vm);
        }

        [Authorize(Policy = Policies.Editor)]
        public IActionResult ListManagement()
        {
            return View(_context.Articles.ToList());
        }

        [Authorize(Policy = Policies.Editor)]
        public IActionResult Create()
        {
            return View(new ArticleCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.Editor)]
        public IActionResult Create(ArticleCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var article = _mapper.Map<Articles>(vm);
                _context.Articles.Add(article);
                _context.SaveChanges();
                SetSuccess("Pomyślnie dodano artykuł");
            }
            catch (Exception e)
            {
                SetError(e);
            }

            return RedirectToAction("ListManagement");
        }

        [Authorize(Policy = Policies.Editor)]
        public IActionResult Edit(int id)
        {
            try
            {
                var article = _context.Articles.Find(id);
                if (article == null)
                {
                    SetError("Nie znaleziono artykułu");
                    return RedirectToAction("ListManagement");
                }
                var vm = _mapper.Map<ArticleCreateViewModel>(article);
                return View(vm);
            }
            catch (Exception e)
            {
                SetError(e);
                return RedirectToAction("ListManagement");
            }
        }

        [Authorize(Policy = Policies.Editor)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(ArticleCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var article = _context.Articles.Find(vm.Id);
                if (article == null)
                {
                    SetError("Nie znaleziono artykułu");
                    return RedirectToAction("ListManagement");
                }
                _mapper.Map(vm, article);
                _context.SaveChanges();
                SetSuccess("Zaktualizowano artykuł");
            }
            catch (Exception e)
            {
                SetError(e);
            }
            return RedirectToAction("ListManagement");
        }

        [Authorize(Policy = Policies.Editor)]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var article = _context.Articles.Find(id);
                if (article == null)
                {
                    SetError("Nie znaleziono artykułu");
                    return RedirectToAction("ListManagement");
                }
                _context.Articles.Remove(article);
                _context.SaveChanges();
                SetSuccess("Usunięto artykuł");
            }
            catch (Exception e)
            {
                SetError(e);
            }
            return RedirectToAction("ListManagement");
        }
    }
}
