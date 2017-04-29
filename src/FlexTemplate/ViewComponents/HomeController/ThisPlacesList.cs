using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisPlacesList : ViewComponent
    {
        private readonly Context _context;

        public ThisPlacesList(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int[] cities, int[] categories, string input, int currentPage, string listType = "")
        {
            var places = _context.Places.AsNoTracking();
            if (cities != null && cities.Any())
                places = places.Where(p => cities.Contains(p.Street.CityId));
            if (categories != null && categories.Any())
                places = places.Where(p => categories.Intersect(p.PlaceCategories.Select(pc => pc.CategoryId)).Any());
            if (!string.IsNullOrEmpty(input))
                places = places.Where(p => p.Name.Contains(input));
            places = places.Include(p => p.PlaceCategories).ThenInclude(pc => pc.Category).ThenInclude(c => c.Aliases)
                .Include(p => p.Reviews)
                .Include(p => p.Aliases)
                .Include(p => p.Street).ThenInclude(s => s.Aliases)
                .Include(p => p.Street).ThenInclude(s => s.City).ThenInclude(c => c.Aliases);
            var thisPagePlaces = places.AsEnumerable().Skip(9 * (currentPage - 1)).Take(9);
            var model = new ThisPlacesListViewModel {Places = thisPagePlaces, Pages = (int)Math.Ceiling((decimal)places.Count() / 9), CurrentPage = currentPage, TotalFoundPlacesCount = places.Count()};
            var type = listType.ToLower() == "grid" ? "Grid" : "List";
            return View(type, model);
        }
    }
}
