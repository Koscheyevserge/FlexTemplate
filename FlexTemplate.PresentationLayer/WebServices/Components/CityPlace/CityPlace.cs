using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.CityPlace
{
    public class CityPlace : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            return View();
            /*var photoPath = "images/1.jpg";
            var place = _context.Places.Include(p => p.Street).Include(p => p.PlaceCategories).ThenInclude(pc => pc.Category).Include(p => p.Reviews).ThenInclude(r => r.User).FirstOrDefault(p => p.Id == id);
            var name = place.Name;
            var address = place.Street.Name;
            var categories = place.PlaceCategories.Select(pc => pc.Category);
            var reviewsCount = place.Reviews.Count();
            var stars = reviewsCount > 0 && place.Reviews.Any(p => p.Star > 0) ? Math.Ceiling(place.Reviews.Where(p => p.Star > 0).Average(p => p.Star)) : 0;
            var model = new ThisCityPlaceViewModel { PlaceId = place.Id, PhotoPath = photoPath, Name = name, Address = address, Categories = categories, Stars = stars, ReviewsCount = reviewsCount };
            return View(model);*/
        }
    }
}
