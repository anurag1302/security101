using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using web.Models;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[Authorize(Roles = "Admin, User")]
        [Authorize(Policy = "ColorCombination")]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return AuthenticateAndCreateClaims(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UnauthenticatedRequest()
        {
            return View();
        }

        private IActionResult AuthenticateAndCreateClaims(LoginViewModel model)
        {
            var authenticatedUser = Repository.Repository.GetUser(model);

            if (authenticatedUser != null)
            {
                var claimsPrincipal = CreatePrincipal(authenticatedUser);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                
                return RedirectToAction("Secret", "Home");
            }

            return RedirectToAction("UnauthenticatedRequest", "Home");
        }

        private static ClaimsPrincipal CreatePrincipal(UserModel authenticatedUser)
        {
            var claims = new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier,authenticatedUser.UserName),
                        new Claim(ClaimTypes.Name,authenticatedUser.UserName),
                        new Claim(ClaimTypes.DateOfBirth,Convert.ToString(authenticatedUser.DateOfBirth)),
                        new Claim(ClaimTypes.Role,authenticatedUser.Role),
                        new Claim("FavColor",authenticatedUser.FavColor),
                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
