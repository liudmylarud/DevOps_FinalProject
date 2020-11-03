using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kotiko.Models;
using Kotiko.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Kotiko.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db;
        public AccountController(UserContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Phone == model.Phone && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Phone); // аутентификация
                    HttpContext.Response.Cookies.Append("Login", model.Phone);

                    return RedirectToAction("Create", "Home");
                }
                ModelState.AddModelError("", "Некорректные номер телефона и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Phone == model.Phone);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Phone = model.Phone, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Phone); // аутентификация
                    HttpContext.Response.Cookies.Append("Login", model.Phone);

                    return RedirectToAction("Create", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные номер телефона и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("Login");
            return RedirectToAction("Index", "Home");
        }
    }
}
