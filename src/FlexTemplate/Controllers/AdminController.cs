using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewComponents;
using FlexTemplate.ViewComponents.AdminController;
using FlexTemplate.ViewModels.AdminController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlexTemplate.Controllers
{
    public class AdminController : BaseController
    {
        #region AdminController
        private readonly Context db;
        private readonly UserManager<User> um;
        private readonly RoleManager<IdentityRole> rm;
        private readonly SignInManager<User> sm;

        private IEnumerable<Container> GetAllContainers()
        {
            var model = db.Containers.Include(c => c.LocalizableStrings)
                        .Include(c => c.Photos)
                        .Include(c => c.ContainerTemplates)
                        .AsNoTracking()
                        .AsEnumerable();
            return model;
        }

        private IEnumerable<Language> GetAllLanguages()
        {
            var model = db.Languages.AsNoTracking().AsEnumerable();
            return model;
        }

        public AdminController(Context context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            db = context;
            rm = roleManager;
            um = userManager;
            sm = signInManager;
        }
        #endregion

        #region Views

        public IActionResult Categories(int id = 1)
        {
            ViewData["Title"] = "Category";
            ViewData["BodyClasses"] = string.Empty;
            id--;
            var model = new AdminCategoriesViewModel
            {
                Categories =
                    db.Categories.Include(i => i.Aliases)
                        .ThenInclude(a => a.Language)
                        .Skip(10 * id)
                        .Take(10)
                        .AsEnumerable(),
                Languages = GetAllLanguages()
            };
            return View(model);
        }

        public IActionResult Pages(int id = 1)
        {
            ViewData["Title"] = "Page";
            ViewData["BodyClasses"] = string.Empty;
            id--;
            var model = new AdminPagesViewModel
            {
                Pages = db.Pages.Include(p => p.PageContainerTemplates)
                        .ThenInclude(pct => pct.ContainerTemplate)
                        .ThenInclude(ct => ct.Container)
                        .ThenInclude(c => c.LocalizableStrings)
                        .ThenInclude(ls => ls.Language)
                        .Include(p => p.PageContainerTemplates)
                        .ThenInclude(pct => pct.ContainerTemplate)
                        .ThenInclude(ct => ct.Container)
                        .ThenInclude(c => c.Photos)
                        .Skip(10 * id)
                        .Take(10)
                        .AsEnumerable(),
                Containers = GetAllContainers(),
                Languages = GetAllLanguages()
            };
            return View(model);
        }

        #endregion

        #region UserRoles

        [HttpGet]
        public IActionResult UserRoles(int id)
        {
            id--;
            var model = db.Roles.Skip(10 * id).Take(10).AsNoTracking();
            return View(model);
        }
        #endregion

        #region Users

        [HttpGet]
        [Route("/Admin/Users/{page}")]
        public IActionResult Users(int page)
        {
            page--;
            var model = db.Users.Skip(10 * page).Take(10).AsNoTracking();
            return View(model);
        }
        #endregion

        #region UserRole

        [HttpGet]
        [Route("/Admin/UserRole/{id}")]
        public IActionResult UserRole(string id)
        {
            var model = db.Roles.FirstOrDefault(i => i.Id == id);
            return View(model);
        }
        #endregion

        #region User

        [HttpPost]
        [Route("api/user/create")]
        public async Task<AjaxResponse> CreateUser([FromBody]User item)
        {
            var result = await um.CreateAsync(item);
            return new AjaxResponse
            {
                ErrorMessages = result.Errors.Select(error => error.Description),
                Successed = result.Succeeded
            };
        }

        #endregion
        
        #region Category
        [HttpGet]
        [Route("api/category/get/{id?}")]
        public JsonResult GetCategory(int id)
        {
            return id == 0 
                ? Json(db.Categories.Include(i => i.Aliases).AsNoTracking()) 
                    : Json(db.Categories.Include(i => i.Aliases).AsNoTracking().SingleOrDefault(i => i.Id == id));
        }

        [HttpGet]
        [Route("api/category/create")]
        public IActionResult CreateCategory()
        {
            return ViewComponent(typeof(NewCategory));
        }

        [HttpGet]
        [Route("api/category/delete/{id}")]
        public JsonResult DeleteCategory(int id)
        {
            try
            {
                if (id == 0) return Json(new AjaxResponse());
                db.Categories.RemoveRange(db.Categories.Where(i => i.Id == id));
                db.SaveChanges();
                return Json(new AjaxResponse{Successed = true});
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse{ErrorMessages = new List<string> { ex.Message }});
            }
        }

        [HttpPost]
        [Route("api/category/update")]
        public JsonResult UpdateCategory([FromBody]Category item)
        {
            try
            {
                if (item == null || item.Id == 0) return Json(new AjaxResponse());
                db.CategoryAliases
                    .RemoveRange(db.CategoryAliases
                        .Where(a => !item.Aliases.Select(i => i.Id).Contains(a.Id) && a.CategoryId == item.Id));
                db.Categories.Update(item);
                db.SaveChanges();
                return Json(new AjaxResponse{Successed = true});
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { ErrorMessages = new List<string> { ex.Message } });
            }
        }

        [HttpGet]
        [Route("api/category/createalias")]
        public IActionResult CreateCategoryAlias()
        {
            return ViewComponent(typeof(NewCategoryAlias));
        }
        #endregion

        #region Page
        [HttpPost]
        [Route("api/page/create")]
        public JsonResult AddPage([FromBody]Page page)
        {
            try
            {
                db.Pages.Add(page);
                db.SaveChanges();
                return Json(page.Id > 0 
                    ? new AjaxCreateResponse {Id = page.Id, Successed = true} 
                        : new AjaxResponse());
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { ErrorMessages = new List<string> { ex.Message } });
            }
        }
        [HttpPost]
        [Route("api/page/update")]
        public JsonResult UpdatePage([FromBody]Page item)
        {
            try
            {
                if (item == null || item.Id == 0) return Json(new AjaxResponse());
                db.PageContainerTemplates
                    .RemoveRange(db.PageContainerTemplates
                        .Where(pct => !item.PageContainerTemplates.Select(i => i.Id).Contains(pct.Id) && pct.PageId == item.Id));
                db.Pages.Update(item);
                db.SaveChanges();
                return Json(new AjaxResponse { Successed = true });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { ErrorMessages = new List<string> { ex.Message } });
            }
        }
        [HttpGet]
        [Route("api/page/delete/{id}")]
        public JsonResult RemovePage(int id)
        {
            try
            {
                if (id == 0) return Json(new AjaxResponse());
                db.Remove(db.Pages.Where(p => p.Id == id));
                db.SaveChanges();
                return Json(new AjaxResponse { Successed = true });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { ErrorMessages = new List<string> { ex.Message } });
            }
        }
        [HttpGet]
        [Route("api/page/get/{id?}")]
        public JsonResult GetPage(int id)
        {
            return id == 0 
                ? Json(db.Pages.Include(p => p.PageContainerTemplates).AsNoTracking())
                    : Json(db.Pages.Include(p => p.PageContainerTemplates).AsNoTracking());
        }
        #endregion
    }
}