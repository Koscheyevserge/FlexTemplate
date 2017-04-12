using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Controllers
{
    public class LoginController : BaseController
    {
        private SignInManager<User> _signInManager { get; set; }
        private IHostingEnvironment _environment { get; set; }

        public LoginController(Context Context, SignInManager<User> signInManager, IHostingEnvironment env) : base(Context)
        {
            context = Context;
            _signInManager = signInManager;
            _environment = env;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LoginAsGuest()
        {
            await _signInManager.SignOutAsync();
            var guest = context.Users.SingleOrDefault(u => u.UserName == "aminailov94");
            await _signInManager.PasswordSignInAsync(guest, "aminailov94", true, false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LoginAsAdmin()
        {
            await _signInManager.SignOutAsync();
            var supervisor = context.Users.SingleOrDefault(u => u.UserName == "Supervisor");
            await _signInManager.PasswordSignInAsync(supervisor, "Supervisor123", true, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
