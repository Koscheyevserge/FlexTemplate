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
using Newtonsoft.Json;


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

        [Route("api/loadmoreplaces")]
        [HttpPost]
        public IActionResult LoadMorePlaces([FromBody]LoadMorePlacesViewModel data)
        {
            return ViewComponent("ThisCityPlaces", new { loadedPlacesIds = data.LoadedPlacesIds });
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
                return NotFound();
            }
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
            var possibleCities =
                context.Cities.Where(
                    c => c.Name.Contains(item.City) || c.Aliases.Any(a => a.Text.Contains(item.City)));
            var chosenCity = possibleCities.Any()
                ? possibleCities.FirstOrDefault()
                : new City { Name = item.City };
            var possibleStreets =
                context.Streets.Where(
                    s => s.Name.Contains(item.Street) || s.Aliases.Any(a => a.Text.Contains(item.Street)));
            var chosenStreet = possibleStreets.Any()
                ? possibleStreets.FirstOrDefault()
                : new Street {Name = item.Street, City = chosenCity };
            var placeCategories = context.Categories.Where(c => item.Categories.Contains(c.Id)).Select(c => new PlaceCategory { Category = c }).ToList();
            var newPlace = new Place
            {
                Address = item.Address,
                Description = item.Description,
                Name = item.Name,
                Email = item.Email,
                Website = item.Website,
                Phone = item.Phone,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                Street = chosenStreet,
                PlaceCategories = placeCategories
            };
            context.Places.Add(newPlace);
            context.SaveChanges();
            var sourceDirectory = $@"wwwroot\Resources\Places\{item.Uid}\";
            var destinationDirectory = $@"wwwroot\Resources\Places\{newPlace.Id}\";
            Directory.Move(sourceDirectory, destinationDirectory);
            return RedirectToAction("Place", new {id = newPlace.Id});
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

        [HttpPost]
        [Route("/api/upload/newplace/{fileDescriptor}")]
        public void UploadNewPlacePhoto(string fileDescriptor)
        {
            var file = HttpContext.Request.Form.Files[0];
            var filename = Guid.NewGuid() + ".jpg";
            var path = $@"wwwroot\Resources\Places\{fileDescriptor}\";
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

        [HttpPost]
        [Route("/api/upload/head/{id}")]
        public void UploadHeadPhoto(string id)
        {
            var file = HttpContext.Request.Form.Files[0];
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

