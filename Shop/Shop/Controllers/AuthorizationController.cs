using BLL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class AuthorizationController : Controller
    {
        IAuthorizationServiceShop authorizationService;
        IUserService userService;
        
        public AuthorizationController(IAuthorizationServiceShop authorizationService, IUserService userService)
        {
            this.authorizationService = authorizationService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (ModelState.IsValid )
            {
                if(await authorizationService.LoginAsync(user.Email, user.Password))
                {
                    await Authenticate(user.Email);
                    return RedirectToAction("Index", "Home");
                }
                
            }
            ModelState.AddModelError("", "Invalid password or email");
            return View(user);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel user)
        {
            if (ModelState.IsValid)
            {
                if (await authorizationService.RegistrateAsync(user.Email, user.Password, user.Login))
                {
                    await Authenticate(user.Email); 
                    return RedirectToAction("Index", "Home");             
                }
            }
            ModelState.AddModelError("", "Invalid data");
            return View(user);
        }

        private async Task Authenticate(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authorization");
        }

    }
}
