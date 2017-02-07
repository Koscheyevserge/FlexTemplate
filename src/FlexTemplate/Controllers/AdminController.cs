using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlexTemplate.Database;

namespace FlexTemplate.Controllers
{
    public class AdminController : Controller
    {

        #region UserRoles

        [HttpGet]
        public IActionResult UserRoles(int id)
        {
            using (var context = new Context(UserRoles))
            {
                var model = context.UserRoles.FirstOrDefault(userRoles => userRoles.Id == id);
                return View(model);
            }
        }
        #endregion

        #region Users

        [HttpGet]
        public IActionResult Users(int id)
        {
            using (var context = new Context(Users))
            {
                var model = context.Users.FirstOrDefault(user => user.Id == id);
                return View(model);
            }
        }
        #endregion

        #region Categories

        [HttpGet]
        public IActionResult Categories(int id)
        {
            using (var context = new Context(Categories))
            {
                var model = context.Categories.FirstOrDefault(categories => categories.Id == id);
                return View(model);
            }
        }
        #endregion
    }
}








