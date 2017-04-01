using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisPlacesFilters : ViewComponent
    {
        private readonly Context _context;

        public ThisPlacesFilters(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int[] cities, int[] categories, string input)
        {
            var model = new ThisPlacesFiltersViewModel {Categories = _context.Categories, Cities = _context.Cities, SelectedCategories = categories, SelectedCities = cities};
            return View(model);
        }
    }
}
