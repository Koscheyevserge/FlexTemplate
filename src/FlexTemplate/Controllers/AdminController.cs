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

        [HttpGet]
        [Route("/api/containers")]
        public IEnumerable<Container> GetAllContainers()
        {
            var model = db.Containers.Include(c => c.LocalizableStrings)
                        .Include(c => c.Photos)
                        .Include(c => c.ContainerTemplates)
                        .AsNoTracking()
                        .AsEnumerable();
            return model;
        }

        [HttpGet]
        [Route("/api/languages")]
        public IEnumerable<Language> GetAllLanguages()
        {
            var model = db.Languages.AsNoTracking().AsEnumerable();
            return model;
        }

        #region Category
        [HttpGet]
        [Route("api/category/{id}")]
        public JsonResult GetCategory(int id)
        {
            return Json(db.Categories.Include(i => i.Aliases).AsNoTracking().SingleOrDefault(i => i.Id == id));
        }

        [HttpPost]
        [Route("api/category/create")]
        public AjaxResponse CreateCategory([FromBody]Category item)
        {
            try
            {
                item.Id = 0;
                item.Aliases = null;
                db.Categories.Add(item);
                db.SaveChanges();
                if (item.Id != 0)
                {
                    return new AjaxCreateResponse
                    {
                        Successed = true,
                        Id = item.Id
                    };
                }
                return new AjaxResponse
                {
                    Successed = false
                };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }

        [HttpGet]
        [Route("api/category/delete/{id}")]
        public AjaxResponse DeleteCategory(int id)
        {
            try
            {
                if (id != 0)
                {
                    var entity = db.Categories.FirstOrDefault(i => i.Id == id);
                    if (entity != null)
                    {
                        db.Categories.Remove(entity);
                        db.SaveChanges();
                        return new AjaxResponse
                        {
                            Successed = true
                        };
                    }
                    return new AjaxResponse
                    {
                        Successed = false, ErrorMessages = new List<string> {"Такой сущности не существует"}
                    };
                }
                return new AjaxResponse
                {
                    Successed = false
                };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }

        [HttpPost]
        [Route("api/category/update")]
        public AjaxResponse UpdateCategory([FromBody]Category item)
        {
            try
            {
                if (item != null && item.Id != 0)
                {
                    item.Aliases = null;
                    db.Categories.Update(item);
                    db.SaveChanges();
                    return new AjaxResponse
                    {
                        Successed = true
                    };
                }
                return new AjaxResponse
                {
                    Successed = false
                };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }

        [HttpPost]
        [Route("api/categoryalias/create")]
        public AjaxResponse CreateCategoryAlias([FromBody]CategoryAlias item)
        {
            try
            {
                item.Id = 0;
                db.CategoryAliases.Add(item);
                db.SaveChanges();
                if (item.Id != 0)
                {
                    return new AjaxCreateResponse
                    {
                        Successed = true,
                        Id = item.Id
                    };
                }
                return new AjaxResponse
                {
                    Successed = false
                };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }

        [HttpGet]
        [Route("api/categoryalias/delete/{id}")]
        public AjaxResponse DeleteCategoryAlias(int id)
        {
            try
            {
                if (id != 0)
                {
                    var entity = db.CategoryAliases.FirstOrDefault(i => i.Id == id);
                    if (entity != null)
                    {
                        db.CategoryAliases.Remove(entity);
                        db.SaveChanges();
                        return new AjaxResponse
                        {
                            Successed = true
                        };
                    }
                    return new AjaxResponse
                    {
                        Successed = false,
                        ErrorMessages = new List<string> { "Такой сущности не существует" }
                    };
                }
                return new AjaxResponse
                {
                    Successed = false
                };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }

        [HttpPost]
        [Route("api/categoryalias/update")]
        public AjaxResponse UpdateCategoryAlias([FromBody]CategoryAlias item)
        {
            try
            {
                if (item.Id != 0)
                {
                    db.CategoryAliases.Update(item);
                    db.SaveChanges();
                    return new AjaxResponse
                    {
                        Successed = true
                    };
                }
                return new AjaxResponse
                {
                    Successed = false
                };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }
        #endregion
        #region Page

        [HttpPost]
        [Route("api/page/create")]
        public AjaxResponse AddPage([FromBody]Page page)
        {
            try
            {
                db.Pages.Add(page);
                db.SaveChanges();
                if (page.Id > 0)
                {
                    return new AjaxCreateResponse {Id = page.Id, Successed = true};
                }
                return new AjaxResponse
                {
                    Successed = false
                };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }
        [HttpPost]
        [Route("api/page/update")]
        public AjaxResponse UpdatePage([FromBody]Page page)
        {
            try
            {
                db.Pages.Update(page);
                db.SaveChanges();
                return new AjaxResponse { Successed = true };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }
        [HttpGet]
        [Route("api/page/delete/{id}")]
        public AjaxResponse RemovePage(int id)
        {
            try
            {
                db.Remove(db.Pages.Where(p => p.Id == id));
                return new AjaxResponse { Successed = true };
            }
            catch (Exception ex)
            {
                return new AjaxResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    Successed = false
                };
            }
        }
        [HttpGet]
        [Route("api/page/{id}")]
        public JsonResult GetPage(int id)
        {
            return Json(db.Pages.Include(p => p.PageContainerTemplates).SingleOrDefault(p => p.Id == id));
        }

        #endregion
    }
}