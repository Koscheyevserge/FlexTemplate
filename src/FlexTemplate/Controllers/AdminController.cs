using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.Services;
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
        private readonly UserManager<User> um;
        private readonly RoleManager<IdentityRole> rm;
        private readonly SignInManager<User> sm;

        private IEnumerable<Container> GetAllContainers()
        {
            var model = context.Containers.Include(c => c.LocalizableStrings)
                        .Include(c => c.Photos)
                        .Include(c => c.ContainerTemplates)
                        .Include(c => c.AvailableContainers)
                        .AsNoTracking()
                        .AsEnumerable();
            return model;
        }

        private IEnumerable<Language> GetAllLanguages()
        {
            var model = context.Languages.AsNoTracking().AsEnumerable();
            return model;
        }

        public AdminController(Context Context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager) : base(Context)
        {
            context = Context;
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
                    context.Categories.Include(i => i.Aliases)
                        .ThenInclude(a => a.Language)
                        .Skip(10 * id)
                        .Take(10)
                        .AsEnumerable(),
                Languages = GetAllLanguages()
            };
            return View(model);
        }

        public IActionResult Containers(int id = 1)
        {
            ViewData["Title"] = "Container";
            ViewData["BodyClasses"] = string.Empty;
            id--;
            var model = new AdminContainersViewModel
            {
                Containers =
                    context.Containers.Include(p => p.LocalizableStrings)
                    .ThenInclude(ls => ls.Language)
                    .Include(c => c.Photos)
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
                Pages = context.Pages.Include(p => p.PageContainerTemplates)
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

        public IActionResult Languages()
        {
            ViewData["Title"] = "Language";
            ViewData["BodyClasses"] = string.Empty;
            var model = new AdminLanguageViewModel
            {
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
            var model = context.Roles.Skip(10 * id).Take(10).AsNoTracking();
            return View(model);
        }
        #endregion

        #region Users

        [HttpGet]
        [Route("/Admin/Users/{page}")]
        public IActionResult Users(int page)
        {
            page--;
            var model = context.Users.Skip(10 * page).Take(10).AsNoTracking();
            return View(model);
        }
        #endregion

        #region UserRole

        [HttpGet]
        [Route("/Admin/UserRole/{id}")]
        public IActionResult UserRole(string id)
        {
            var model = context.Roles.FirstOrDefault(i => i.Id == id);
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
                context.Categories.RemoveRange(context.Categories.Where(i => i.Id == id));
                context.SaveChanges();
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
                context.CategoryAliases
                    .RemoveRange(context.CategoryAliases
                        .Where(a => !item.Aliases.Select(i => i.Id).Contains(a.Id) && a.CategoryId == item.Id));
                context.Categories.Update(item);
                context.SaveChanges();
                return Json(new AjaxResponse{Successed = true});
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { ErrorMessages = new List<string> { ex.Message } });
            }
        }

        [HttpGet]
        [Route("api/category/createalias/{id}")]
        public IActionResult CreateCategoryAlias(int id)
        {
            return ViewComponent(typeof(NewCategoryAlias), new {id});
        }
        #endregion

        #region Page
        [HttpPost]
        [Route("api/page/update")]
        public async Task<JsonResult> UpdatePage([FromBody]Page item)
        {
            try
            {
                if (item == null || item.Id == 0) return Json(new AjaxResponse());
                var entity = context.Pages.Include(p => p.PageContainerTemplates).Single(p => p.Id == item.Id);
                var pageContainerTemplatesToRemove = item.PageContainerTemplates.Where(pct => !item.PageContainerTemplates.Select(itempct => itempct.Id).Contains(pct.Id));
                entity.PageContainerTemplates = item.PageContainerTemplates.Where(pct => item.PageContainerTemplates.Select(itempct => itempct.Id).Contains(pct.Id)).ToList();
                context.PageContainerTemplates.RemoveRange(pageContainerTemplatesToRemove);
                entity.Title = item.Title;
                await context.SaveChangesAsync();
                return Json(new AjaxResponse { Successed = true });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { ErrorMessages = new List<string> { ex.Message } });
            }
        }
        [HttpGet]
        [Route("api/page/createcontainer/{id}")]
        public IActionResult CreatePageContainerTemplate(int id)
        {
            return ViewComponent(typeof(NewPageContainer), new { id });
        }
        #endregion

        #region Language
        [HttpPost]
        public IActionResult Languages(AdminLanguagePostViewModel item)
        {
            return RedirectToAction("Languages");
        }
        #endregion

        #region LocalizableString
        [HttpPost]
        [Route("api/localizablestrings/update")]
        public JsonResult UpdateLocalizableString([FromBody]AdminUpdateLocalizableStringViewModel model)
        {
            var localizableString = context.ContainerLocalizableStrings.SingleOrDefault(ls => ls.Id == model.Id);
            if (localizableString == null)
            {
                return Json(new AjaxResponse {ErrorMessages = new List<string> {"Такой локализируемой строки не существует"}});
            }
            localizableString.Text = model.Item;
            context.SaveChanges();
            return Json(new AjaxResponse {Successed = true});
        }
        #endregion
    }
}