using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FlexTemplate.ViewComponents
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
            var categoryNames = _context.Categories.Select(c => c.Name).ToList();
            var photoPath = "images/hero-header/01.jpg";
            var model = new SearchViewModel { CategoriesNames = categoryNames, PhotoPath = photoPath };
            return View(string.IsNullOrEmpty(template) ? "Default" : template, model);
        }
    }
}
