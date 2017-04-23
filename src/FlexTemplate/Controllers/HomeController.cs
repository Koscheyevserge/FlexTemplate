using System;
using System.Collections.Generic;
using System.Globalization;
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
                    context.Places
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
                        .Include(p => p.Schedule)
                        .Include(p => p.Menus)
                        .ThenInclude(m => m.Products)
                        .SingleOrDefault(item => item.Id == id)
            };
            return View(model);
        }

        public IActionResult Blogs()
        {
            ViewData["Title"] = "Blogs";
            ViewData["BodyClasses"] = "full-width-container";
            var model = new HomeBlogsViewModel
            {
               Blogs = context.Blogs.Include(a => a.Author)
               .Include(c => c.Comments)
               .Take(4)
            };
            return View(model);
        }

        public IActionResult Blog(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ViewData["Title"] = "Blog";
            ViewData["BodyClasses"] = "full-width-container";
            var model = new HomeBlogViewModel
            {
                Blog = context.Blogs.Include(blog => blog.Author)
                .SingleOrDefault(blog => blog.Id == id),

                Comments = context.BlogComments.Where(com => com.BlogId == id)
                .Include(a => a.Author),
            };
            return View(model);
        }

        public IActionResult NewPlace()
        {
            ViewData["Title"] = "NewPlace";
            ViewData["BodyClasses"] = "full-width-container";
            return View(context.Categories.AsNoTracking().AsEnumerable());
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
                : new City { Name = item.City, Country = context.Countries.FirstOrDefault()};
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
                Latitude = double.Parse(item.Latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(item.Longitude, CultureInfo.InvariantCulture),
                Street = chosenStreet,
                PlaceCategories = placeCategories
            };
            if (!(item.MondayFrom == TimeSpan.Zero && item.MondayTo == TimeSpan.Zero
                && item.TuesdayFrom == TimeSpan.Zero && item.TuesdayTo == TimeSpan.Zero
                && item.WednesdayFrom == TimeSpan.Zero && item.WednesdayTo == TimeSpan.Zero
                && item.ThurstdayFrom == TimeSpan.Zero && item.ThurstdayTo == TimeSpan.Zero
                && item.FridayFrom == TimeSpan.Zero && item.FridayTo == TimeSpan.Zero
                && item.SaturdayFrom == TimeSpan.Zero && item.SaturdayTo == TimeSpan.Zero
                && item.SundayFrom == TimeSpan.Zero && item.SundayTo == TimeSpan.Zero)
                && item.MondayFrom <= item.MondayTo && item.TuesdayFrom <= item.TuesdayTo
                && item.WednesdayFrom <= item.WednesdayTo && item.ThurstdayFrom <= item.ThurstdayTo
                && item.FridayFrom <= item.FridayTo && item.SaturdayFrom <= item.SaturdayTo && item.SundayFrom <= item.SundayTo)
            {
                newPlace.Schedule = new Schedule
                {
                    MondayFrom = item.MondayFrom,
                    MondayTo = item.MondayTo,
                    TuesdayFrom = item.TuesdayFrom,
                    TuesdayTo = item.TuesdayTo,
                    WednesdayFrom = item.WednesdayFrom,
                    WednesdayTo = item.WednesdayTo,
                    ThurstdayFrom = item.ThurstdayFrom,
                    ThurstdayTo = item.ThurstdayTo,
                    FridayFrom = item.FridayFrom,
                    FridayTo = item.FridayTo,
                    SaturdayFrom = item.SaturdayFrom,
                    SaturdayTo = item.SaturdayTo,
                    SundayFrom = item.SundayFrom,
                    SundayTo = item.SundayTo
                };
            }
            newPlace.Menus = item.Menus
                .Where(m => m.Products.Any(p => p.Price > 0 && !string.IsNullOrEmpty(p.Name)))
                .Select(menu => 
                new Menu
                {
                    Name = menu.Name,
                    Products = menu.Products
                    .Where(p => p.Price > 0 && !string.IsNullOrEmpty(p.Name))
                    .Select(product =>
                    new Product
                    {
                        Description = product.Description,
                        Price = product.Price,
                        Title = product.Name
                    }).ToList()
                }).ToList();
            context.Places.Add(newPlace);
            context.SaveChanges();
            string destinationDirectory = null;
            string sourceDirectory = null;
            foreach (var menu in newPlace.Menus)
            {
                foreach (var product in menu.Products)
                {
                    var entity = item.Menus.SelectMany(m => m.Products)
                        .FirstOrDefault(p => p.Description == product.Description && Math.Abs(p.Price - product.Price) < 0.1 && p.Name == product.Title);
                    var uid = entity?.Guid;
                    if(uid == null)
                        continue;
                    sourceDirectory = $@"wwwroot\Resources\Products\{uid}.tmp";
                    if (System.IO.File.Exists(sourceDirectory))
                    {
                        destinationDirectory = $@"wwwroot\Resources\Products\{product.Id}.jpg";
                        if (System.IO.File.Exists(destinationDirectory))
                        {
                            Directory.Delete(destinationDirectory, true);
                        }
                        System.IO.File.Move(sourceDirectory, destinationDirectory);
                    }
                }
            }
            sourceDirectory = $@"wwwroot\Resources\Places\{item.Uid}\";
            if (Directory.Exists(sourceDirectory))
            {
                destinationDirectory = $@"wwwroot\Resources\Places\{newPlace.Id}\";
                if (System.IO.File.Exists(destinationDirectory))
                {
                    Directory.Delete(destinationDirectory, true);
                }
                Directory.Move(sourceDirectory, destinationDirectory);
            }
            return RedirectToAction("Place", new {id = newPlace.Id});
        }

        public IActionResult EditPlace(int id)
        {
            var place = context.Places
                .Include(p => p.Schedule)
                .Include(p => p.PlaceCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Street).ThenInclude(s => s.City)
                .Include(p => p.Menus).ThenInclude(s => s.Products)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
            if (place == null)
            {
                return NotFound();
            }
            var model = new EditPlaceViewModel
            {
                Street = place.Street.Name,
                Name = place.Name,
                Address = place.Address,
                AllCategories = context.Categories.AsNoTracking().AsEnumerable(),
                City = place.Street?.City?.Name,
                CurrentCategories = place.PlaceCategories?.Select(pc => pc?.Category),
                Description = place.Description,
                Email = place.Email,
                Longitude = place.Longitude.ToString("0.00", CultureInfo.InvariantCulture),
                Latitude = place.Latitude.ToString("0.00", CultureInfo.InvariantCulture),
                Website = place.Website,
                Phone = place.Phone,
                Menus = place.Menus
            };
            if (place.Schedule != null)
            {
                model.MondayFrom = place.Schedule.MondayFrom;
                model.MondayTo = place.Schedule.MondayTo;
                model.TuesdayFrom = place.Schedule.TuesdayFrom;
                model.TuesdayTo = place.Schedule.TuesdayTo;
                model.WednesdayFrom = place.Schedule.WednesdayFrom;
                model.WednesdayTo = place.Schedule.WednesdayTo;
                model.ThurstdayFrom = place.Schedule.ThurstdayFrom;
                model.ThurstdayTo = place.Schedule.ThurstdayTo;
                model.FridayFrom = place.Schedule.FridayFrom;
                model.FridayTo = place.Schedule.FridayTo;
                model.SaturdayFrom = place.Schedule.SaturdayFrom;
                model.SaturdayTo = place.Schedule.SaturdayTo;
                model.SundayFrom = place.Schedule.SundayFrom;
                model.SundayTo = place.Schedule.SundayTo;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPlace(string test)
        {
            return null;
        }

        public IActionResult ChangeUserLanguage(string redirect, int languageId)
        {
            return Redirect(redirect);
        }
    }
}