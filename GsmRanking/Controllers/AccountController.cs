using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GsmRanking.Common;
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
        private readonly DbContext _context;

        public AccountController(DbContext context)
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

                //_userService.RegisterUser(viewModel.UserName, viewModel.Email, viewModel.Password);
                //LogInfo(Events.Account_Register);
                SetSuccess("Rejestracja zakończona sukcesem.");
            }
            catch (Exception e)
            {
                //LogException(Events.Account_Register, e);
                SetError(e);
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                //var user = await _userService.LoginUserAsync(viewModel.Login, viewModel.Password);
                //var claimsPrincipal = CreateClaims(user);

                //await HttpContext
                //    .Authentication
                //    .SignInAsync(Startup.AuthenticationScheme, claimsPrincipal,
                //        new AuthenticationProperties
                //        {
                //            IsPersistent = viewModel.RememberMe
                //        });

                //LogInfo(Events.Account_Login);
                SetSuccess("Zalogowałeś się!");

                return Ok();
            }
            catch (Exception e)
            {
                //LogException(Events.Account_Login, e);
                SetError(e);
                return BadRequest(e);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(Startup.AuthenticationScheme);
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

        //[NonAction]
        //private ClaimsPrincipal CreateClaims(User user)
        //{
        //    var privileges = _userService.GetUserPrivileges(user);
        //    var roles = user.Roles;

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Username),
        //        new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email),
        //        new Claim(nameof(Privilege), privileges.ToString()),
        //    };
        //    claims.AddRange(roles.Select(x => x.Role).Select(x => new Claim(ClaimTypes.Role, x.Code)));

        //    var claimsIdentity = new ClaimsIdentity(claims);
        //    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        //    return claimsPrincipal;
        //}
    }
}
