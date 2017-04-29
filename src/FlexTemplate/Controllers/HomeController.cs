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

        public IActionResult Places(int[] cities, int[]categories, string input, int currentPage = 1, string listType = "")
        {
            if (listType == null)
            {
                listType = String.Empty;
            }
            ViewData["Title"] = "Places";
            ViewData["BodyClasses"] = "full-width-container";
            return View(new HomePlacesViewModel {Categories = categories, Cities = cities, Input = input, CurrentPage = currentPage, ListType = listType});
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

        public IActionResult Blogs(int id = 1)
        {
            ViewData["Title"] = "Blogs";
            ViewData["BodyClasses"] = "full-width-container";
            var total = context.Blogs.Count();
            var model = new HomeBlogsViewModel
            {
                Blogs = context.Blogs.Include(a => a.Author)
               .Include(c => c.Comments)
               .Skip(6 * (id - 1))
               .Take(6)
               .AsEnumerable(),
                CurrentPage = id,
                Pages = (int)Math.Ceiling((decimal)total / 6),
                TotalFoundBlogsCount = total
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

        public IActionResult NewBlog()
        {
            ViewData["Title"] = "NewBlog";
            ViewData["BodyClasses"] = "full-width-container";
            return View();
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
                Latitude = double.Parse(item.Latitude ?? "50.5", CultureInfo.InvariantCulture),
                Longitude = double.Parse(item.Longitude ?? "30.5", CultureInfo.InvariantCulture),
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
            ViewData["Title"] = "EditPlace";
            ViewData["BodyClasses"] = "full-width-container";
            var model = context.Places
                .Include(p => p.Street).ThenInclude(s => s.City)
                .Include(p => p.Menus).ThenInclude(s => s.Products)
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(place => new EditPlaceViewModel
                {
                    Id = place.Id,
                    Street = place.Street != null ? place.Street.Name : string.Empty,
                    Name = place.Name,
                    Address = place.Address,
                    Description = place.Description,
                    Email = place.Email,
                    Longitude = place.Longitude.ToString("0.000000", CultureInfo.InvariantCulture),
                    Latitude = place.Latitude.ToString("0.000000", CultureInfo.InvariantCulture),
                    Website = place.Website,
                    Phone = place.Phone
                })
                .SingleOrDefault();
            if (model == null)
            {
                return NotFound();
            }
            var entity = context.Places
                .Include(p => p.Street).ThenInclude(s => s.City)
                .Include(p => p.Menus).ThenInclude(s => s.Products)
                .Include(p => p.Schedule)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
            if (entity != null)
            {
                model.MondayFrom = entity.Schedule?.MondayFrom ?? TimeSpan.Zero;
                model.MondayTo = entity.Schedule?.MondayTo ?? TimeSpan.Zero;
                model.TuesdayFrom = entity.Schedule?.TuesdayFrom ?? TimeSpan.Zero;
                model.TuesdayTo = entity.Schedule?.TuesdayTo ?? TimeSpan.Zero;
                model.WednesdayFrom = entity.Schedule?.WednesdayFrom ?? TimeSpan.Zero;
                model.WednesdayTo = entity.Schedule?.WednesdayTo ?? TimeSpan.Zero;
                model.ThurstdayFrom = entity.Schedule?.ThurstdayFrom ?? TimeSpan.Zero;
                model.ThurstdayTo = entity.Schedule?.ThurstdayTo ?? TimeSpan.Zero;
                model.FridayFrom = entity.Schedule?.FridayFrom ?? TimeSpan.Zero;
                model.FridayTo = entity.Schedule?.FridayTo ?? TimeSpan.Zero;
                model.SaturdayFrom = entity.Schedule?.SaturdayFrom ?? TimeSpan.Zero;
                model.SaturdayTo = entity.Schedule?.SaturdayTo ?? TimeSpan.Zero;
                model.SundayFrom = entity.Schedule?.SundayFrom ?? TimeSpan.Zero;
                model.SundayTo = entity.Schedule?.SundayTo ?? TimeSpan.Zero;
            }
            var categories = context.Categories.AsEnumerable();
            model.AllCategories = categories ?? new List<Category>().AsEnumerable();
            model.CurrentCategories = context.PlaceCategories.Where(pc => pc.PlaceId == id).Select(pc => pc.Category);
            var city = context.Places
                .Include(p => p.Street)
                .ThenInclude(s => s.City)
                .SingleOrDefault(p => p.Id == id);
            if (city != null)
                model.City = city.Street.City.Name;
            model.Menus = context.Places.Include(p => p.Menus).ThenInclude(m => m.Products).Where(p => p.Id == id)
                .SelectMany(p => p.Menus.Select(m => new EditPlaceMenuViewModel { Id = m.Id, Name = m.Name })).ToList();
            model.Menus.ForEach(
                menu => menu.Products = context.Products.Where(prod => prod.MenuId == menu.Id)
                    .Select(prod =>
                        new EditPlaceProductViewModel
                        {
                            Description = prod.Description,
                            Id = prod.Id,
                            Price = prod.Price,
                            Name = prod.Title,
                            HasPhoto = System.IO.File.Exists($"wwwroot/Resources/Products/{prod.Id}.jpg")
                        })
                    .ToList());
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPlacePost(EditPlaceViewModel item)
        {
            var place = context.Places
                .Include(p => p.Menus).ThenInclude(m => m.Products)
                .Include(p => p.PlaceCategories).ThenInclude(pc => pc.Category)
                .SingleOrDefault(p => p.Id == item.Id);
            if (place == null)
            {
                return NotFound();
            }
            var possibleCities =
                context.Cities.Where(
                    c => c.Name.Contains(item.City) || c.Aliases.Any(a => a.Text.Contains(item.City)));
            var chosenCity = possibleCities.Any()
                ? possibleCities.FirstOrDefault()
                : new City { Name = item.City, Country = context.Countries.FirstOrDefault() };
            var possibleStreets =
                context.Streets.Where(
                    s => s.Name.Contains(item.Street) || s.Aliases.Any(a => a.Text.Contains(item.Street)));
            var chosenStreet = possibleStreets.Any()
                ? possibleStreets.FirstOrDefault()
                : new Street { Name = item.Street, City = chosenCity };
            var placeCategories = context.Categories.Where(c => item.Categories.Contains(c.Id)).Select(c => new PlaceCategory { Category = c }).ToList();
            place.Address = item.Address;
            place.Description = item.Description;
            place.Name = item.Name;
            place.Email = item.Email;
            place.Website = item.Website;
            place.Phone = item.Phone;
            place.Latitude = double.Parse(item.Latitude ?? "50.5", CultureInfo.InvariantCulture);
            place.Longitude = double.Parse(item.Longitude ?? "30.5", CultureInfo.InvariantCulture);
            place.Street = chosenStreet;
            context.PlaceCategories.RemoveRange(place.PlaceCategories);
            place.PlaceCategories = placeCategories;
            if (item.MondayFrom <= item.MondayTo && item.TuesdayFrom <= item.TuesdayTo
                && item.WednesdayFrom <= item.WednesdayTo && item.ThurstdayFrom <= item.ThurstdayTo
                && item.FridayFrom <= item.FridayTo && item.SaturdayFrom <= item.SaturdayTo && item.SundayFrom <= item.SundayTo)
            {
                place.Schedule = new Schedule
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
            var nonExsistingMenus = place.Menus.Where(menu => !item.Menus.Select(m => m.Id).Contains(menu.Id));
            context.Menus.RemoveRange(nonExsistingMenus);
            var nonExsistingProducts = new List<Product>();
            foreach (var menu in place.Menus.Except(nonExsistingMenus))
            {
                var newMenu = item.Menus.First(m => m.Id == menu.Id);
                nonExsistingProducts.AddRange(menu.Products.Where(product => !newMenu.Products.Select(p => p.Id).Contains(product.Id) || product.Price > 0 && !string.IsNullOrEmpty(product.Title)));
            }
            context.Products.RemoveRange(nonExsistingProducts);
            foreach (var menu in item.Menus)
            {
                var exsistingMenu = place.Menus.SingleOrDefault(m => m.Id == menu.Id);
                if (exsistingMenu == null)
                {
                    exsistingMenu = new Menu {Name = menu.Name, Products = new List<Product>()};
                    foreach (var product in menu.Products.Where(product => product.Price > 0 && !string.IsNullOrEmpty(product.Name)))
                    {
                        exsistingMenu.Products.Add(new Product {Description = product.Description, Title = product.Name, Price = product.Price});
                    }
                    if (exsistingMenu.Products.Any())
                    {
                        place.Menus.Add(exsistingMenu);
                    }
                }
                else
                {
                    foreach (var product in menu.Products)
                    {
                        var exsistingProduct = exsistingMenu.Products.SingleOrDefault(p => p.Id == product.Id);
                        if (exsistingProduct == null)
                        {
                            if (product.Price > 0 && !string.IsNullOrEmpty(product.Name))
                            {
                                exsistingMenu.Products.Add(new Product
                                {
                                    Description = product.Description,
                                    Title = product.Name,
                                    Price = product.Price
                                });
                            }
                        }
                        else
                        {
                            exsistingProduct.Description = product.Description;
                            exsistingProduct.Title = product.Name;
                            exsistingProduct.Price = product.Price;
                        }
                    }
                }
            }
            context.Places.Update(place);
            context.SaveChanges();
            foreach (var menu in place.Menus)
            {
                foreach (var product in menu.Products)
                {
                    var entity = item.Menus.SelectMany(m => m.Products)
                        .FirstOrDefault(p => p.Description == product.Description && Math.Abs(p.Price - product.Price) < 0.1 && p.Name == product.Title);
                    if(entity == null)
                        continue;
                    var uid = entity.Guid.ToString();
                    if (uid == Guid.Empty.ToString())
                        uid = entity.Id.ToString();
                    string sourceDirectory = $@"wwwroot\Resources\Products\{uid}.tmp";
                    if (System.IO.File.Exists(sourceDirectory))
                    {
                        string destinationDirectory = $@"wwwroot\Resources\Products\{product.Id}.jpg";
                        if (System.IO.File.Exists(destinationDirectory))
                        {
                            Directory.Delete(destinationDirectory, true);
                        }
                        System.IO.File.Move(sourceDirectory, destinationDirectory);
                    }
                }
            }
            return RedirectToAction("Place", "Home", new {id = place.Id});
        }

        public IActionResult ChangeUserLanguage(string redirect, int languageId)
        {
            return Redirect(redirect);
        }
    }
}