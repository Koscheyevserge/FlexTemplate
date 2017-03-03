using System;
using System.Linq;
using FlexTemplate.Database;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisCityPlace : ViewComponent
    {
        private readonly Context _context;

        public ThisCityPlace(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var photoPath = "images/hot-item/01.jpg";
            var place = _context.Places.Where(p => p.Id == id).Include(p => p.Street).Include(p => p.PlaceCategories).ThenInclude(pc => pc.Category).Include(p => p.Reviews).FirstOrDefault();
            var name = place.Name;
            var address = place.Street.Name;
            var categories = place.PlaceCategories.Select(c => c.Category.Name).ToList();
            var reviewsCount = place.Reviews.Count();
            var stars = reviewsCount > 0 ? Math.Ceiling(place.Reviews.Average(p => p.Star)) : 0;
            var model = new ThisCityPlaceViewModel { PhotoPath = photoPath, Name = name, Address = address, Categories = categories, Stars = stars, ReviewsCount = reviewsCount };
            return View(model);
        }
    }
}
