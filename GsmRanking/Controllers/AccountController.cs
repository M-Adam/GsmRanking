using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using GsmRanking.Common;
using GsmRanking.Common.Enums;
using GsmRanking.Models;
using GsmRanking.Viewmodels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GsmRanking.Controllers
{
    [AllowAnonymous]
    public class AccountController : GsmRankingBaseController
    {
        private readonly GsmRankingContext _context;

        public AccountController(GsmRankingContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                _context.Users.Add(new User()
                {
                    UserPassword = Hasher.HashPassword(viewModel.Password),
                    Username = viewModel.UserName,
                    Email = viewModel.Email,
                    UserType = (byte) UserTypeEnum.Viewer
                });
                _context.SaveChanges();
                SetSuccess("Rejestracja zakończona sukcesem.");
            }
            catch (Exception e)
            {
                SetError(e);
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                User user;
                if (viewModel.Login.Contains("@"))
                {
                    user = _context.Users.FirstOrDefault(x => x.Email == viewModel.Login);
                }
                else
                {
                    user = _context.Users.FirstOrDefault(x => x.Username == viewModel.Login);
                }
                if (user == null)
                {
                    return BadRequest("Nie znaleziono użytkownika");
                }
                if (!Hasher.IsPasswordValid(user.UserPassword, viewModel.Password))
                {
                    return BadRequest("Niepoprawne hasło");
                }

                var claimsPrincipal = CreateClaims(user);

                HttpContext
                    .Authentication
                    .SignInAsync(Startup.AuthenticationScheme, claimsPrincipal,
                        new AuthenticationProperties
                        {
                            IsPersistent = viewModel.RememberMe
                        })
                    .Wait();

                SetSuccess("Zalogowałeś się!");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                SetError(e);
                return BadRequest(e);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync(Startup.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Unauthorized(string returnUrlParameter)
        {
            if (!User.IsLoggedIn())
            {
                SetError("Nie masz wystarczających uprawnień.");
                return RedirectToAction("Index", "Home");
            }
            return LocalRedirect(returnUrlParameter);
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [NonAction]
        private ClaimsPrincipal CreateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
            };
            
            var claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return claimsPrincipal;
        }
    }
}
