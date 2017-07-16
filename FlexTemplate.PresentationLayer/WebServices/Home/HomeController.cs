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

        public IActionResult NewBlog()
        {
            var model = BllServices.GetNewBlogDto();
            return View(model.To<NewBlog.ViewModel>());
        }
        
        [HttpPost]
        public async Task<IActionResult> NewBlog(NewBlog.PostModel model)
        {
            var newBlogId = await BllServices.CreateBlogAsync(HttpContext.User, model.To<CreateBlogDto>());
            return RedirectToAction("Blog", "Home", new {id = newBlogId});
        }
        
        [HttpPost]
        public async Task<IActionResult> NewBlogComment(Blog.NewBlogCommentPostModel model)
        {
            //TODO сделать вызов через Ajax без перезагрузки страницы
            var result = await BllServices.AddCommentAsync(HttpContext.User, model.To<NewBlogCommentDto>());
            return RedirectToAction("Blog", new { id = model.BlogId});
        }
        
        [HttpPost]
        public async Task<IActionResult> NewPlaceReview(Place.NewPlaceReviewPostModel model)
        {
            //TODO сделать вызов через Ajax без перезагрузки страницы
            var result = await BllServices.AddReviewAsync(HttpContext.User, model.To<NewPlaceReviewDto>());
            return RedirectToAction("Place", new { id = model.PlaceId });
        }
        
        [HttpPost]
        public async Task<IActionResult> NewPlace(NewPlace.PostModel model)
        {
            var newPlaceId = await BllServices.CreatePlaceAsync(HttpContext.User, model.To<NewPlaceDto>());
            return RedirectToAction("Place", new {id = newPlaceId});
        }
        
        public async Task<IActionResult> EditBlog(int id)
        {
            var model = await BllServices.GetEditBlogAsync(HttpContext.User, id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model.To<EditBlog.ViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(EditBlog.PostModel item)
        {
            var result = await BllServices.EditBlogAsync(item.To<EditBlogDto>());
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction("Blog", "Home", new { id = item.Id });
        }
        
        public async Task<IActionResult> EditPlace(int id)
        {
            var model = await BllServices.GetEditPlaceAsync(HttpContext.User, id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model.To<EditPlace.ViewModel>());
        }
        
        [HttpPost]
        public async Task<IActionResult> EditPlace(EditPlace.PostModel item)
        {
            var result = await BllServices.EditPlaceAsync(item.To<EditPlaceDto>());
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction("Blog", "Home", new { id = item.Id });
        }
        /*
        public IActionResult ChangeUserLanguage(string redirect, int languageId)
        {
            return Redirect(redirect);
        }*/
    }
}