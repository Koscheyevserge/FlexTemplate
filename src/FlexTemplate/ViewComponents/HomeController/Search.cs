using System.Collections.Generic;
using System.Linq;
using FlexTemplate.Database;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var categoryNames = _context.Categories.Select(c => c.Name).ToList();
            var photoPath = new List<string>{ "images/hero-header/01.jpg"};
            var strings = _context.Containers.Include(c => c.LocalizableStrings)
                .FirstOrDefault(c => c.Name == GetType().Name)
                .LocalizableStrings.ToDictionary(ls => ls.Tag, ls => ls.Text);
            var model = new SearchViewModel { CategoriesNames = categoryNames, Images = photoPath, Strings = strings};
            return View(template, model);
        }
    }
}
