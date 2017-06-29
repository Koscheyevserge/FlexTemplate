using System.Threading.Tasks;
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
            var hierarchy = await BllServices.GetPageContainersHierarchyAsync(ControllerContext.ActionDescriptor.ActionName);
            var canEdit = await BllServices.CanEditVisualsAsync(HttpContext.User);
            var placesOnPageIds = await BllServices.GetPlacesAsync(HttpContext.User, cities, categories, input, currentPage, orderBy, isDescending);
            var placesTotal = await BllServices.GetPlacesCountAsync(cities, categories, input);
            var model = new Places.ViewModel
            {
                Hierarchy = hierarchy,
                CanEditVisuals = canEdit,
                PlacesOnPageIds = placesOnPageIds,
                PlacesTotal = placesTotal
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
            var getBlogsAsync = await BllServices.GetBlogsAsync(tags, categories, input, currentPage);
            return View(getBlogsAsync.To<Blogs.ViewModel>());
        }
        /*
        public async Task<IActionResult> Blog(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = new Blog.ViewModel
            {
                
            };
            return View(model);
        }

        public IActionResult NewBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewBlog(NewBlogViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var inputTags = model.Tags != null && model.Tags.Any() ? model.Tags.Split(',').Select(tag => tag.Trim()).ToList() : new List<string>{""};
            var tags =
                context.Tags.Where(
                    tag =>
                        inputTags.Contains(tag.Name) || inputTags.Intersect(tag.TagAliases.Select(ta => ta.Text)).Any())
                            .Select(tag => new BlogTag {Tag = tag}).ToList();
            var newBlog = new Blog {Caption = model.Name, Preamble = model.Preamble, Text = model.Text, Author = user, BlogTags = tags, CreatedOn = DateTime.Now};
            context.Blogs.Add(newBlog);
            await context.SaveChangesAsync();
            FilesProvider.MoveFolder($@"wwwroot\Resources\Blogs\{model.Guid}\", $@"wwwroot\Resources\Blogs\{newBlog.Id}\");
            return RedirectToAction("Blog", "Home", new {id = newBlog.Id});
        }

        [HttpPost]
        public async Task<IActionResult> NewBlogComment(NewBlogCommentViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var newBlogComment = new BlogComment { BlogId = model.BlogId, Author = user, CreatedOn = DateTime.Now, Text = model.Text };
            context.BlogComments.Add(newBlogComment);
            await context.SaveChangesAsync();
            return RedirectToAction("Blog", new {id = model.BlogId});
        }

        [HttpPost]
        public async Task<IActionResult> NewPlaceReview(NewPlaceReviewViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var newPlaceReview = new PlaceReview { PlaceId = model.PlaceId, User = user, CreatedOn = DateTime.Now, Text = model.Text };
            context.PlaceReviews.Add(newPlaceReview);
            await context.SaveChangesAsync();
            return RedirectToAction("Place", new { id = model.PlaceId });
        }

        public IActionResult NewPlace()
        {
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
            var placeFeatures = new List<PlaceFeature>();
            for (var i = 0; i < item.Features.Length; i++)
            {
                for (var j = 0; j < item.Features[i].Length; j++)
                {
                    if (!string.IsNullOrEmpty(item.Features[i][j]))
                    {
                        placeFeatures.Add(new PlaceFeature {Name = item.Features[i][j], Row = i + 1, Column = j + 1});
                    }
                }
            }
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
                PlaceCategories = placeCategories,
                PlaceFeatures = placeFeatures
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
            foreach (var menu in newPlace.Menus)
            {
                foreach (var product in menu.Products)
                {
                    var entity = item.Menus.Where(m => m.Name == menu.Name).SelectMany(m => m.Products)
                        .FirstOrDefault(p => p.Description == product.Description && Math.Abs(p.Price - product.Price) < 0.1 && p.Name == product.Title);
                    var uid = entity?.Guid;
                    if (uid != null)
                    {
                        FilesProvider.MoveFile($@"wwwroot\Resources\Products\{uid}.tmp", $@"wwwroot\Resources\Products\{product.Id}.jpg");
                    }
                }
            }
            FilesProvider.MoveFolder($@"wwwroot\Resources\Places\{item.Uid}\", $@"wwwroot\Resources\Places\{newPlace.Id}\");
            return RedirectToAction("Place", new {id = newPlace.Id});
        }

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