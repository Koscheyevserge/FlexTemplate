using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.Services;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Context context { get; set; }

        protected BaseController(Context Context)
        {
            context = Context;
        }

        public override void OnActionExecuting(ActionExecutingContext _context)
        {
            ViewData["HeaderTemplate"] = "Solid";
            ViewBag.Query = Request.Query.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
            base.OnActionExecuting(_context);
        }
    }
}
