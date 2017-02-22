using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Services;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FlexTemplate.Controllers
{
    public class HomeController : Controller
    {
        private Context context { get; set; }

        public HomeController(Context Context)
        {
            context = Context;
        }

        public IActionResult Index()
        {
            var page = context.Pages.AsNoTracking().Where(p => p.Name == "Index")
                .Include(p => p.PageContainers).ThenInclude(pc => pc.Container).ThenInclude(c => c.LocalizableStrings)
                .Include(p => p.LocalizableStrings)
                .FirstOrDefault();
            ViewData["Title"] = page.Title;
            ViewData["BodyClasses"] = page.BodyClasses;
            var containers = page.PageContainers.OrderBy(pc => pc.Position).ToDictionary(pc => pc.Container.Name, pc => pc.Container.TemplateName);
            var strings = page.LocalizableStrings.ToDictionary(ls => ls.Tag, ls => ls.Text);
            var model = new HomeIndexViewModel
            {
                Containers = containers,
                Strings = strings
            };
            return View(model);
        }

        public IActionResult Error()
        {
            ViewData["Title"] = "Oops!";
            ViewData["BodyClasses"] = "full-width-container";
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }
    }
}

