using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Search
{
    public class Search : ViewComponent
    {
        public IViewComponentResult Invoke(string templateName)
        {
            /*var categories = _context.Categories.ToList();
            var cities = _context.Cities.ToList();
            var photoPath = new List<string>{ "images/2.jpg"};
            var strings = LocalizableStringsProvider.GetStrings(_context, GetType().Name, User.IsInRole("Supervisor"));
            var model = new SearchViewModel { Categories = categories, Cities = cities, Images = photoPath, Strings = strings};
            return View(template, model);*/
            return View(templateName);
        }
    }
}