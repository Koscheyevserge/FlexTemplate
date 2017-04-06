using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FlexTemplate.Database;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FlexTemplate.Services;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class Search : ViewComponent
    {
        private readonly Context _context;

        public Search(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string template)
        {
            var categories = _context.Categories.ToList();
            var cities = _context.Cities.ToList();
            var photoPath = new List<string>{ "images/2.jpg"};
            var strings = LocalizableStringsProvider.GetStrings(_context, GetType().Name, User.IsInRole("Supervisor"));
            var model = new SearchViewModel { Categories = categories, Cities = cities, Images = photoPath, Strings = strings};
            return View(template, model);
        }
    }
}
