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
        protected Language CurrentUserLanguage { get; set; }

        protected BaseController(Context Context)
        {
            context = Context;
        }

        public override void OnActionExecuting(ActionExecutingContext _context)
        {
            ViewData["HeaderTemplate"] = "Solid";
            CurrentUserLanguage = context.Languages.FirstOrDefault(uc => uc.ShortName == CookieProvider.GetLanguage(HttpContext)) ?? context.Languages.FirstOrDefault(l => l.IsDefault);
            ViewBag.HeaderViewModel = new HeaderViewModel{Languages = context.Languages.AsNoTracking(), CurrentLanguage = CurrentUserLanguage ?? new Language()};
            base.OnActionExecuting(_context);
        }
    }
}
