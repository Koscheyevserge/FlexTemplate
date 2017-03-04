using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.Services;
using FlexTemplate.ViewModels;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;


namespace FlexTemplate.Controllers
{
    public class HomeController : BaseController
    {
        private Context context { get; set; }
        private SignInManager<User> _signInManager { get; set; }

        public HomeController(Context Context, SignInManager<User> signInManager)
        {
            context = Context;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var page = context.Pages.AsNoTracking().Where(p => p.Name == "Index")
                .Include(p => p.PageContainerTemplates).ThenInclude(pct => pct.ContainerTemplate).ThenInclude(ct => ct.Container).ThenInclude(c => c.LocalizableStrings)
                .FirstOrDefault();
            ViewData["Title"] = page.Title;
            ViewData["BodyClasses"] = page.BodyClasses;
            var containers = page.PageContainerTemplates.OrderBy(pc => pc.Position).Select(pct => new KeyValuePair<string, string>(pct.ContainerTemplate.Container.Name, pct.ContainerTemplate.TemplateName));
            var model = new HomeIndexViewModel
            {
                Containers = containers
            };
            return View(model);
        }

        public IActionResult Error()
        {
            ViewData["Title"] = "Oops!";
            ViewData["BodyClasses"] = "full-width-container";
            return View();
        }

        public IActionResult Users()
        {
            return View();
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

