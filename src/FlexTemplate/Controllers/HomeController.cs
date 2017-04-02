using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.Services;
using FlexTemplate.ViewModels;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;


namespace FlexTemplate.Controllers
{
    public class HomeController : BaseController
    {
        private SignInManager<User> _signInManager { get; set; }
        private IHostingEnvironment _environment { get; set; }

        public HomeController(Context Context, SignInManager<User> signInManager, IHostingEnvironment env) : base(Context)
        {
            context = Context;
            _signInManager = signInManager;
            _environment = env;
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

        public IActionResult Places(int[] cities, int[]categories, string input)
        {
            ViewData["Title"] = "Places";
            ViewData["BodyClasses"] = "full-width-container";
            return View(new HomePlacesViewModel {Categories = categories, Cities = cities, Input = input});
        }

        public IActionResult Place(int id)
        {
            if (id == 0)
            {
                id = CookieProvider.GetCurrentPlace(HttpContext);
                UploadPlaceHead(id, HttpContext.Request.Form.Files[0]);
                return null;
            }
            CookieProvider.AppendCurrentPlace(HttpContext, id);
            ViewData["Title"] = "Place";
            ViewData["BodyClasses"] = "full-width-container";
            var model = new HomePlaceViewModel
            {
                Place =
                    context.Places.Include(p => p.Photos)
                        .Include(p => p.PlaceCategories)
                        .ThenInclude(pc => pc.Category)
                        .Include(p => p.Reviews)
                        .ThenInclude(r => r.User)
                        .Include(p => p.Street)
                        .ThenInclude(s => s.City)
                        .ThenInclude(c => c.Country)
                        .ThenInclude(c => c.Aliases)
                        .Include(p => p.Street)
                        .ThenInclude(s => s.City)
                        .ThenInclude(c => c.Aliases)
                        .Include(p => p.Street)
                        .ThenInclude(s => s.Aliases)
                        .SingleOrDefault(item => item.Id == id)
            };
            return View(model);
        }

        public IActionResult Blogs()
        {
            ViewData["Title"] = "Blogs";
            ViewData["BodyClasses"] = "full-width-container";
            return View();
        }

        public IActionResult Blog()
        {
            ViewData["Title"] = "Blog";
            ViewData["BodyClasses"] = "full-width-container";
            return View();
        }

        public IActionResult NewPlace()
        {
            ViewData["Title"] = "NewPlace";
            ViewData["BodyClasses"] = "full-width-container";
            return View();
        }

        [HttpPost]
        public IActionResult NewPlace(NewPlacePostViewModel item)
        {
            ViewData["Title"] = "NewPlace";
            ViewData["BodyClasses"] = "full-width-container";
            var newPlaceId = 0;
            return RedirectToAction("Place", new {id = newPlaceId});
        }

        public IActionResult ChangeUserLanguage(string redirect, int languageId)
        {
            return Redirect(redirect);
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

        public void UploadPlaceFiles(int id, IFormFile file)
        {
            var filename = Guid.NewGuid().ToString() + ".jpg";
            var path = $@"wwwroot\Resources\Places\{id}\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (file.Length > 0)
            {
                using (var stream = new FileStream(path + filename, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
            }
        }

        public void UploadPlaceHead(int id, IFormFile file)
        {
            var path = $@"wwwroot\Resources\Places\{id}\";
            var filename = "head.jpg";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (file.Length > 0)
            {
                using (var stream = new FileStream(path + filename, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
            }
        }
    }
}

