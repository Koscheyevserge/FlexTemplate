using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Home
{
    public class HomeController : FlexController
    {
        public HomeController(ControllerServices services) : base(services)
        {

        }

        public async Task<IActionResult> Index()
        {
            var model = new Index.ViewModel
            {
                Hierarchy = await BllServices.GetPageContainersHierarchyAsync(ControllerContext.ActionDescriptor.ActionName),
                CanEditVisuals = await BllServices.CanEditVisualsAsync(HttpContext.User)
            };
            return View(model);
        }
        
        public IActionResult Error()
        {
            return View();
        }
        
        public async Task<IActionResult> Places(int[] cities, int[]categories, string input, int currentPage = 1, int listType = 1, int orderBy = 1, bool isDescending = false)
        {
            var model = new Places.ViewModel
            {
                Hierarchy = await BllServices.GetPageContainersHierarchyAsync(ControllerContext.ActionDescriptor.ActionName),
                CanEditVisuals = await BllServices.CanEditVisualsAsync(HttpContext.User),
                PlacesOnPageIds = await BllServices.GetPlacesAsync(HttpContext.User, cities, categories, input, currentPage, orderBy, isDescending),
                PlacesTotal = await BllServices.GetPlacesCountAsync(cities, categories, input),
                BannerPhotoPath = ""
            };
            return View(model);
        }
        
        public async Task<IActionResult> Place(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = new Place.ViewModel
            {
                Hierarchy = await BllServices.GetPageContainersHierarchyAsync(ControllerContext.ActionDescriptor.ActionName),
                CanEditVisuals = await BllServices.CanEditVisualsAsync(HttpContext.User),
                IsAuthor = await BllServices.IsAuthorAsync<DataAccessLayer.Entities.Place>(HttpContext.User, id)
            };
            return View(model);
        }
        
        public async Task<IActionResult> Blogs(int[] tags, int[] categories, string input, int currentPage = 1)
        {
            var getBlogsAsync = await BllServices.GetBlogsAsync(HttpContext.User, tags, categories, input, currentPage);
            return View(getBlogsAsync.To<Blogs.ViewModel>());
        }
        
        public async Task<IActionResult> Blog(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = await BllServices.GetBlogAsync(HttpContext.User, id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model.To<Blog.ViewModel>());
        }

        public async Task<IActionResult> NewPlace()
        {
            var model = await BllServices.GetNewPlaceDtoAsync(HttpContext.User);
            return View(model.To<NewPlace.ViewModel>());
        }

        public async Task<IActionResult> NewBlog()
        {
            var model = await BllServices.GetNewBlogDtoAsync();
            return View(model.To<NewBlog.ViewModel>());
        }
        
        [HttpPost]
        public async Task<IActionResult> NewBlog(NewBlog.PostModel model)
        {
            var newBlogId = await BllServices.CreateBlog(HttpContext.User, model.To<CreateBlogDto>());
            return RedirectToAction("Blog", "Home", new {id = newBlogId});
        }
        
        [HttpPost]
        public async Task<IActionResult> NewBlogComment(Blog.NewBlogCommentPostModel model)
        {
            //TODO сделать вызов через Ajax без перезагрузки страницы
            var result = await BllServices.AddComment(HttpContext.User, model.To<NewBlogCommentDto>());
            return RedirectToAction("Blog", new { id = model.BlogId});
        }
        
        [HttpPost]
        public async Task<IActionResult> NewPlaceReview(Place.NewPlaceReviewPostModel model)
        {
            //TODO сделать вызов через Ajax без перезагрузки страницы
            var result = await BllServices.AddReview(HttpContext.User, model.To<NewPlaceReviewDto>());
            return RedirectToAction("Place", new { id = model.PlaceId });
        }
        
        [HttpPost]
        public IActionResult NewPlace(NewPlace.PostModel model)
        {
            //TODO var newPlaceId = await BllServices.CreatePlace(HttpContext.User, model.To<NewPlaceDto>());
            return RedirectToAction("Place", new {id = 1});
        }
        /*
        public async Task<IActionResult> EditBlog(int id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                return NotFound();
            }
            var blogs = context.Blogs.Where(b => b.Author == currentUser && b.Id == id).Select(b => new EditBlogViewModel { Id = b.Id, Preamble = b.Preamble, Name = b.Caption}).ToList();
            if (blogs.Count > 1 || blogs.Count == 0)
            {
                return NotFound();
            }
            return View(blogs.Single());
        }

        [HttpPost]
        public IActionResult EditBlogPost(EditBlog.PostModel item)
        {
            var blog = context.Blogs.SingleOrDefault(b => b.Id == item.Id);
            return RedirectToAction("Blog", "Home", new { id = blog.Id });
        }

        public IActionResult EditPlace(int id)
        {
            var model = context.Places
                .Include(p => p.Street).ThenInclude(s => s.City)
                .Include(p => p.Menus).ThenInclude(s => s.Products)
                .Include(p => p.PlaceFeatures)
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
                    Phone = place.Phone,
                    Features = place.PlaceFeatures.OrderBy(pf => pf.Row).GroupBy(pf => pf.Row).Select(ig => ig.OrderBy(f => f.Column).Select(f => f.Name).ToArray()).ToArray()
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
        public IActionResult EditPlacePost(EditPlace.PostModel item)
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
            var placeFeatures = new List<PlaceFeature>();
            context.PlaceFeatures.RemoveRange(context.PlaceFeatures.Where(f => f.PlaceId == place.Id));
            for (var i = 0; i < item.Features.Length; i++)
            {
                for (var j = 0; j < item.Features[i].Length; j++)
                {
                    if (!string.IsNullOrEmpty(item.Features[i][j]))
                    {
                        placeFeatures.Add(new PlaceFeature { Name = item.Features[i][j], Row = i + 1, Column = j + 1 });
                    }
                }
            }
            place.PlaceFeatures = placeFeatures;
            var nonExsistingMenus = place.Menus.Where(menu => !item.Menus.Select(m => m.Id).Contains(menu.Id));
            context.Menus.RemoveRange(nonExsistingMenus);
            var nonExsistingProducts = new List<Product>();
            foreach (var menu in place.Menus.Except(nonExsistingMenus))
            {
                var newMenu = item.Menus.First(m => m.Id == menu.Id);
                nonExsistingProducts.AddRange(menu.Products.Where(product => !newMenu.Products.Select(p => p.Id).Contains(product.Id) || product.Price == 0 || string.IsNullOrEmpty(product.Title)));
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
                    FilesProvider.MoveFile($@"wwwroot\Resources\Products\{uid}.tmp", $@"wwwroot\Resources\Products\{product.Id}.jpg");                
                }
            }
            return RedirectToAction("Place", "Home", new {id = place.Id});
        }

        public IActionResult ChangeUserLanguage(string redirect, int languageId)
        {
            return Redirect(redirect);
        }*/
    }
}