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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FlexTemplate.Controllers
{
    public class AdminController : Controller
    {
        private readonly Context db;
        private readonly UserManager<User> um;
        private readonly RoleManager<IdentityRole> rm; 

        public AdminController(Context context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            rm = roleManager;
            um = userManager;
        }

        #region UserRoles

        [HttpGet]
        [Route("/Admin/UserRoles/{page}")]
        public IActionResult UserRoles(int page)
        {
            page--;
            var model = db.Roles.Skip(10 * page).Take(10).AsNoTracking();
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

        [HttpGet]
        [Route("/Admin/User/{id}")]
        public new IActionResult User(string id)
        {
            var model = db.Users.FirstOrDefault(i => i.Id == id);
            return View(model);
        }

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
        [Route("/Admin/Category/{id}")]
        public IActionResult Category(int id)
        {
            var model = GetCategory(id);
            return View(model);
        }

        [HttpGet]
        [Route("/Admin/Categories/{page}")]
        public IActionResult Categories(int page)
        {
            page--;
            var model = db.Categories.Skip(10 * page).Take(10).AsNoTracking();
            return View(model);
        }

        [HttpGet]
        [Route("api/category/{id}")]
        public Category GetCategory(int id)
        {
            return db.Categories.Include(i => i.Aliases).AsNoTracking().FirstOrDefault(i => i.Id == id);
        }

        [HttpPost]
        [Route("api/category/create")]
        public AjaxResponse CreateCategory([FromBody]Category item)
        {
            try
            {
                item.Id = 0;
                db.Categories.Add(item);
                db.SaveChanges();
                if (item.Id != 0)
                {
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

        [HttpGet]
        [Route("api/category/delete/{id}")]
        public AjaxResponse DeleteCategory(int id)
        {
            try
            {
                if (id != 0)
                {
                    db.Categories.FirstOrDefault(i => i.Id == id);
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
        [Route("api/category/update")]
        public AjaxResponse UpdateCategory([FromBody]Category item)
        {
            try
            {
                if (item.Id != 0)
                {
                    item.Id = 0;
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
        #endregion
    }
}