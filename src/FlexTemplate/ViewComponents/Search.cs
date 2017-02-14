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
        private UserManager<User> _userManager;

        public Search(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var categoryNames = _context.Categories.Select(c => c.Name).ToList();
            var photoPath = "images/hero-header/01.jpg";
            var model = new SearchViewModel { CategoriesNames = categoryNames, PhotoPath = photoPath };
            return View(model);
        }
    }
}
