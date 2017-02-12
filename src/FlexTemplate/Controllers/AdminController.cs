﻿using System;
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
        private readonly Context db;

        public AdminController(Context context)
        {
            db = context;
        }

        #region UserRoles

        [HttpGet]
        public IActionResult UserRoles()
        {
            var model = db.UserRoles.Take(10);
            return View(model);
        }
        #endregion

        #region Users

        [HttpGet]
        public IActionResult Users()
        {
            var model = db.Users.Take(10);
            return View(model);
        }
        #endregion

        #region Categories

        [HttpGet]
        public IActionResult Categories()
        {
            var model = db.Categories.Take(10).Include(ur => ur.Aliases);
            return View(model);
        }
        #endregion

        #region UserRole

        [HttpGet]
        public IActionResult UserRole(int id)
        {
            var model = db.UserRoles.FirstOrDefault();
            return View(model);
        }
        #endregion


        #region User

        [HttpGet]
        public new IActionResult User(int id)
        {
            var model = db.Users.FirstOrDefault();
            return View(model);
        }
        #endregion

        #region Category

        [HttpGet]
        public IActionResult Category(int id)
        {
            var model = db.Categories.FirstOrDefault();
            return View(model);
        }
        #endregion
    }
}