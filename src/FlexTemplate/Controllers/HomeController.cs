using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace FlexTemplate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "TestTitle";
            ViewData["BodyClasses"] = "full-width-container transparent-header";
            var model = new HomeIndexViewModel();
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

